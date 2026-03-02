using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleSystem
{
    public int CurrentBehaviourCount
    {
        get => _currentBehavourCount;
        set
        {
            var prevBahaviourCount = _currentBehavourCount;
            _currentBehavourCount = value;
            if (_currentBehavourCount != prevBahaviourCount) {

            }
        }
    }

    // 추후 우선순위 조정을 위해 우선순위 큐로 container 구현
    private BattleStateController stateController = new BattleStateController();
    private StrategyBehaviourContainerBase strategyBehaviourContainer = new ListStrategyBehaviourContainer();
    private BehaviourResolver behaviourResolver = new BehaviourResolver();
    private int _currentBehavourCount = 0;

    public void RegisterBehaviour(StrategyBehaviour strategyBehaviour) {
        strategyBehaviourContainer.RegisterBehaviour(strategyBehaviour);
    }
    public void RemoveBehaviour(StrategyBehaviour strategyBehaviour) {
        strategyBehaviourContainer.RemoveBehaviour(strategyBehaviour);
    }
    public bool TryPullBehaviour(out StrategyBehaviour strategyBehaviour) {
        return strategyBehaviourContainer.TryPullBehaviour(out strategyBehaviour);
    }


    public void PrepareBattle(BattleEntity player, BattleEntity enemy, Action onBattleReadied=null) {
        if (player == null || enemy == null) { Debug.LogError($"entity empty. player null? {player == null}, enemy null? {enemy==null}"); return; }

        stateController.OnStateChanged -= UpdateState;
        stateController.OnStateChanged += UpdateState;

        Managers.Time.TurnTimer.OnTimeChanged -= Managers.Turn.UpdateTurn;
        Managers.Time.TurnTimer.OnTimeChanged += Managers.Turn.UpdateTurn;

        Managers.Turn.OnTurnChanged -= ResolveTurnBehaviours;
        Managers.Turn.OnTurnChanged += ResolveTurnBehaviours;

        player.OnStateChanged -= ApplyState;
        player.OnStateChanged += ApplyState;
        enemy.OnStateChanged -= ApplyState;
        enemy.OnStateChanged += ApplyState;

        if (!stateController.TryTransitionTo(Define.BattleState.Ready)) 
            { Debug.LogError($"<color=red>failed to transtion to ({Define.BattleState.Ready})</color>"); }

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

    private void ResolveTurnBehaviours(int _, int __) {
        PauseTurn();
        behaviourResolver.ResolveTurnBehaviours(strategyBehaviourContainer, 
            onResolveCompleted: () => {
                ResumeTurn();
            });
    }
    private void PauseTurn() {
        Managers.Time.TurnTimer.Pause(true);
    }
    private void ResumeTurn() {
        Managers.Time.TurnTimer.Pause(false);
    }
    private void ApplyState(BattleEntity.BehaviourState currentState, BattleEntity.BehaviourState prevState) {
        switch (currentState) {
            case BattleEntity.BehaviourState.Idle:
                --_currentBehavourCount;
                break;
            case BattleEntity.BehaviourState.Selected:
                ++_currentBehavourCount;
                break;
            default:
                break;
        }
    }


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

    private void UnsubscribeAll() {
        stateController.OnStateChanged -= UpdateState;
        Managers.Time.TurnTimer.OnTimeChanged -= Managers.Turn.UpdateTurn;
        Managers.Turn.OnTurnChanged -= ResolveTurnBehaviours;
        //player.OnStateChanged -= ApplyState;
        //enemy.OnStateChanged -= ApplyState;
    }

    private void ExitState(Define.BattleState state) {
        switch (state) {
            case Define.BattleState.InProgress:
                behaviourResolver.Cancel(); // 진행 중 강제 종료 대비
                UnsubscribeAll();
                break;
        }
    }
}