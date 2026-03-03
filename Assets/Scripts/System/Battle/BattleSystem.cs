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

    #region Behaviour Control

    public void RegisterBehaviour(StrategyBehaviour strategyBehaviour) {
        if (!CanRegister()) return;
        behaviourContainer.RegisterBehaviour(strategyBehaviour);

        if (behaviourContainer.CurrentBehaviourCount == 2) {
            Managers.Turn.RaiseTurn();
        }
    }

    private bool CanRegister() {
        return stateController.CurrentState == Define.BattleState.Wait;
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
    private void OnTurnChanged(int _, int __) => ResolveTurnBehaviours();

    #endregion


    #region Battle Flow

    public void PrepareBattle(BattleEntity player, BattleEntity enemy, Action onBattleReadied = null) {
        if (player == null || enemy == null) { Debug.LogError($"entity empty. player null? {player == null}, enemy null? {enemy == null}"); return; }

        stateController.OnStateChanged -= UpdateState;
        stateController.OnStateChanged += UpdateState;

        Managers.Time.TurnTimer.OnTimeChanged -= Managers.Turn.UpdateTurn;
        Managers.Time.TurnTimer.OnTimeChanged += Managers.Turn.UpdateTurn;

        Managers.Turn.OnTurnChanged -= OnTurnChanged;
        Managers.Turn.OnTurnChanged += OnTurnChanged;

        if (!stateController.TryTransitionTo(Define.BattleState.Ready)) { Debug.LogError($"<color=red>failed to transtion to ({Define.BattleState.Ready})</color>"); }

        Managers.UI.OpenUI<BattleUI>().InitUI(player, enemy);

        Managers.Turn.InitTurn();

        // battle 시작 연출
        // ---



        onBattleReadied?.Invoke();
    }

    public void StartBattle() {
        if (!stateController.TryTransitionTo(Define.BattleState.Wait)) { return; }
    }
    public void FinishBattle() {
        if (!stateController.TryTransitionTo(Define.BattleState.Finish)) { return; }
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
                break;
        }
    }
    private void ExitState(Define.BattleState state) {
        switch (state) {
            case Define.BattleState.Wait:
                behaviourResolver.Cancel(); // 진행 중 강제 종료 대비
                UnsubscribeAll();
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