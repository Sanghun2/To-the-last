using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleSystem
{
    private BattleStateController stateController = new BattleStateController();
    // 추후 우선순위 조정을 위해 우선순위 큐로 container 구현
    private StrategyBehaviourContainerBase behaviourContainer = new ListStrategyBehaviourContainer();
    private BehaviourResolver behaviourResolver = new BehaviourResolver();
    private BattleEntityManager battleEntityManager = new BattleEntityManager();
    private EffectProcessorRegistry effectProcessorRegistry = new EffectProcessorRegistry();

    public event Action OnBattleEntered;
    public event Action OnBattleStarted;
    public event Action OnBattleFinished;
    public event Action OnBattleExit;

    #region Context Resolve

    public BattleEntity GetFirstEnemy() {
        return battleEntityManager.GetFirstEnemy();
    }
    public BattleEntity GetPlayerEntity() {
        return battleEntityManager.GetPlayerEntity();
    }

    public Entity ResolveTarget(Entity caster, Effect.TargetType targetType) {

        var target = ResolveTarget(caster as BattleEntity, targetType);

        //
        //

        return target;
    }
    public Entity ResolveTarget(BattleEntity caster, Effect.TargetType targetType) {
        BattleEntity playerEntity = battleEntityManager.GetPlayerEntity();

        if (caster.Equals(playerEntity)) {
            return targetType switch {
                Effect.TargetType.None => null,
                Effect.TargetType.Self => battleEntityManager.GetPlayerEntity(),
                Effect.TargetType.ClosestEnemy => battleEntityManager.GetFirstEnemy(),
                _ => throw new Exception($"<color=red>resolve target type ({targetType}) is not defined</color>")
            };
        }
        else {
            return targetType switch {
                Effect.TargetType.None => null,
                Effect.TargetType.Self => battleEntityManager.GetFirstEnemy(),
                Effect.TargetType.ClosestEnemy => battleEntityManager.GetPlayerEntity(),
                _ => throw new Exception($"<color=red>resolve target type ({targetType}) is not defined</color>")
            };
        }
    }

    public EffectProcessor GetEffectProcessor(IEffect type) {
        if (effectProcessorRegistry.TryGet(type, out var effectProcessor)) {
            return effectProcessor;
        }

        Debug.Log($"({type.GetType()})에 맞는 processor가 등록되어 있지 않아 default processor로 처리 진행");
        return effectProcessorRegistry.DefaultProcessor;
    }

    #endregion

    #region Behaviour Control

    public void RegisterBehaviour(StrategyBehaviour strategyBehaviour) {
        if (!CanRegister()) return;
        behaviourContainer.RegisterBehaviour(strategyBehaviour);

        if (behaviourContainer.CurrentBehaviourCount == 2) {
            Managers.Turn.RaiseTurn();
        }
    }
    public void RegisterBehaviour(List<StrategyBehaviour> strategyBehaviours) {
        for (int i = 0; i < strategyBehaviours.Count; i++) {
            RegisterBehaviour(strategyBehaviours[i]);
        }
    }
    public void RemoveBehaviour(StrategyBehaviour strategyBehaviour) {
        behaviourContainer.RemoveBehaviour(strategyBehaviour);
    }
    public bool TryPullBehaviour(out StrategyBehaviour strategyBehaviour) {
        return behaviourContainer.TryPullBehaviour(out strategyBehaviour);
    }


    private void ResolveTurnBehaviours() {
        if (!stateController.TryTransitionTo(Define.BattleState.Resolve)) { Debug.LogError($"<color=red>resolve 진입을 시도했으나 실패.</color>"); return; }

        behaviourResolver.ResolveTurnBehaviours(behaviourContainer,
            onResolveCompleted: () => {
                if (!stateController.TryTransitionTo(Define.BattleState.Wait)) {
                    Debug.LogError($"<color=red>resolve 후 wait 진입 실패</color>");
                }
            });
    }
    private void OnTurnChanged(int currentTurn, int __) {
        if (currentTurn > 1) {
            ResolveTurnBehaviours();
        }
    }
    private bool CanRegister() {
        return stateController.CurrentState == Define.BattleState.Wait;
    }

    #endregion


    #region Battle Flow

    public void PrepareBattle(BattleEntity player, BattleEntity enemy, Action onBattleReadied = null) {
        if (player == null || enemy == null) { Debug.LogError($"entity empty. player null? {player == null}, enemy null? {enemy == null}"); return; }

        OnBattleEntered?.Invoke();

        stateController.ResetEvent();
        stateController.OnStateChanged -= UpdateState;
        stateController.OnStateChanged += UpdateState;

        Managers.Time.TurnTimer.OnTimeChanged -= Managers.Turn.UpdateTurn;
        Managers.Time.TurnTimer.OnTimeChanged += Managers.Turn.UpdateTurn;

        Managers.Turn.OnTurnChanged -= OnTurnChanged;
        Managers.Turn.OnTurnChanged += OnTurnChanged;

        if (!stateController.TryTransitionTo(Define.BattleState.Ready)) { Debug.LogError($"<color=red>failed to transtion to ({Define.BattleState.Ready})</color>"); }

        battleEntityManager.RegisterPlayer(player);
        battleEntityManager.RegisterEnemy(enemy);
        var battleUI = Managers.UI.OpenUI<BattleUI>();

        stateController.OnStateChanged += player.ResetState;
        stateController.OnStateChanged += enemy.ResetState;

        battleUI.InitUI(player, enemy);
        battleUI.InitSkillUI(Managers.Player.PlayerData.SkillList);
        Managers.Turn.InitTurn();

        battleEntityManager.OnEnemyRemoved -= CheckBattleMaintanance;
        battleEntityManager.OnEnemyRemoved += CheckBattleMaintanance;

        // battle 시작 연출
        battleUI.ShowAnimation(() => {
            onBattleReadied?.Invoke();
        });
    }
    public void StartBattle() {
        if (!stateController.TryTransitionTo(Define.BattleState.Wait)) { return; }

        OnBattleStarted?.Invoke();

        // 전투 시작 애니메이션 및 처리
        //
        // ---
    }
    public void FinishBattle() {
        if (!stateController.TryTransitionTo(Define.BattleState.Finish)) { return; }

        Debug.Log("전투 종료");
        OnBattleFinished?.Invoke();

        // 전투 종료 애니메이션 및 처리
        //
        // ---
    }
    public void ExitBattle() {
        if (!stateController.TryTransitionTo(Define.BattleState.Exit)) { return; }

        OnBattleExit?.Invoke();

        Managers.UI.CloseUI<BattleUI>();
    }

    private void CheckBattleMaintanance(int remainEnemyCount) {
        if (remainEnemyCount == 0) {
            FinishBattle();
        }
    }

    #endregion


    #region Turn Control

    private void PauseTurnTimer() {
        Managers.Time.TurnTimer.Pause(true);
    }
    private void ResumeTurnTimer() {
        Managers.Time.TurnTimer.Pause(false);
    }
    private void PauseMainTimer() {
        Managers.Time.PauseTime(true);
    }
    private void ResumeMainTimer() {
        Managers.Time.PauseTime(false);
    }

    #endregion


    #region State Handle

    private void UpdateState(Define.BattleState current, Define.BattleState prev) {
        ExitState(prev);
        EnterState(current);
    }
    private void EnterState(Define.BattleState state) {
        switch (state) {
            case Define.BattleState.Ready:
                PauseMainTimer();
                Managers.Time.TurnTimer.InitTime(0);
                PauseTurnTimer();
                break;
            case Define.BattleState.Wait:
                ResumeTurnTimer();
                break;
            case Define.BattleState.Resolve:
                PauseTurnTimer();
                break;
            case Define.BattleState.Finish:
                ResumeMainTimer();
                behaviourResolver.Cancel();
                UnsubscribeAll();
                break;
        }
    }
    private void ExitState(Define.BattleState state) {
        switch (state) {
            case Define.BattleState.Wait:
                break;
        }
    }

    #endregion


    #region Event Subscription

    private void UnsubscribeAll() {
        stateController.OnStateChanged -= UpdateState;
        Managers.Time.TurnTimer.OnTimeChanged -= Managers.Turn.UpdateTurn;
        Managers.Turn.OnTurnChanged -= OnTurnChanged;
    }


    #endregion

}