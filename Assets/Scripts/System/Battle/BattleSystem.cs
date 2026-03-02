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
        behaviourContainer.RegisterBehaviour(strategyBehaviour);

        if (behaviourContainer.CurrentBehaviourCount == 2) {
            ResolveTurnBehaviours();
        }
    }
    public void RemoveBehaviour(StrategyBehaviour strategyBehaviour) {
        behaviourContainer.RemoveBehaviour(strategyBehaviour);
    }
    public bool TryPullBehaviour(out StrategyBehaviour strategyBehaviour) {
        return behaviourContainer.TryPullBehaviour(out strategyBehaviour);
    }

    private void ResolveTurnBehaviours() {
        PauseTurn();
        behaviourResolver.ResolveTurnBehaviours(behaviourContainer,
            onResolveCompleted: () => {
                ResumeTurn();
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
        if (!stateController.TryTransitionTo(Define.BattleState.InProgress)) { return; }
    }
    public void FinishBattle() {
        if (!stateController.TryTransitionTo(Define.BattleState.Finish)) { return; }
    }

    #endregion


    #region Turn Control

    private void PauseTurn() {
        Managers.Time.TurnTimer.Pause(true);
    }
    private void ResumeTurn() {
        Managers.Time.TurnTimer.Pause(false);
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
                Managers.Time.PauseTime(true);
                Managers.Time.TurnTimer.InitTime(0);
                PauseTurn();
                break;
            case Define.BattleState.InProgress:
                ResumeTurn();
                break;
            case Define.BattleState.Finish:
                Managers.Time.PauseTime(false);
                break;
        }
    }
    private void ExitState(Define.BattleState state) {
        switch (state) {
            case Define.BattleState.InProgress:
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