using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem
{
    public enum State {
        Ready,
        InProgress,
        Finish,
    }

    public State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            UpdateState(value);
        }
    }

    // 추후 우선순위 조정을 위해 우선순위 큐로 container 구현
    private StrategyBehaviourContainerBase strategyBehaviourContainer = new ListStrategyBehaviourContainer(); 
    private State _currentState;
    private Guid? resolveRoutineID;
    //private BattleEntity player;
    //private BattleEntity enemy;

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

        Managers.Time.TurnTimer.OnTimeChanged -= Managers.Turn.UpdateTurn;
        Managers.Time.TurnTimer.OnTimeChanged += Managers.Turn.UpdateTurn;

        Managers.Turn.OnTurnChanged -= ResolveTurnBehaviours;
        Managers.Turn.OnTurnChanged += ResolveTurnBehaviours;


        CurrentState = State.Ready;
        Managers.UI.OpenUI<BattleUI>().InitUI(player, enemy);

        Managers.Turn.InitTurn();

        // battle 시작 연출
        // ---



        onBattleReadied?.Invoke();
    }
    public void StartBattle() {
        CurrentState = State.InProgress;
    }
    public void FinishBattle() {
        CurrentState = State.Finish;
    }

    private void ResolveTurnBehaviours(int currentTurn, int delta) {
        PauseTurn();
        resolveRoutineID = Managers.Coroutine.StartCoroutine(StrategyBehaviourResolveRoutine(() => {
            ResumeTurn();
            resolveRoutineID = null;
        }));
    }
    private void UpdateState(State state) {
        switch (state) {
            case State.Ready:
                Managers.Time.PauseTime(true);
                PauseTurn();
                Managers.Time.TurnTimer.InitTime(0);
                break;
            case State.InProgress:
                ResumeTurn();
                break;
            case State.Finish:
                Managers.Time.PauseTime(false);
                break;
            default:
                break;
        }
    }
    private IEnumerator StrategyBehaviourResolveRoutine(Action onResolveCompleted=null) {
        while (TryPullBehaviour(out var strategyBehaviour)) {
            bool isCompleted = false;
            strategyBehaviour.Resolve(() => isCompleted = true);
            while (!isCompleted) {
                yield return null;
            }
        }

        onResolveCompleted?.Invoke();
    }
    private void PauseTurn() {
        Managers.Time.TurnTimer.Pause(true);
    }
    private void ResumeTurn() {
        Managers.Time.TurnTimer.Pause(false);
    }
}