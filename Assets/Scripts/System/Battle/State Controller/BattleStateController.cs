using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class BattleStateController
{
    public Define.BattleState CurrentState
    {
        get => _currentState;
        set
        {
            var prevState = _currentState;
            _currentState = value;

            if (_currentState != prevState) {
                OnStateChanged?.Invoke(_currentState, prevState);
            }
        }
    }

    public event Action<Define.BattleState, Define.BattleState> OnStateChanged;

    private Define.BattleState _currentState;
    private static readonly Dictionary<Define.BattleState, HashSet<Define.BattleState>> validTransitions = new Dictionary<Define.BattleState, HashSet<Define.BattleState>>() {
        { Define.BattleState.Ready,      new HashSet<Define.BattleState>{ Define.BattleState.None, Define.BattleState.Finish} },
        { Define.BattleState.Wait, new HashSet<Define.BattleState>{ Define.BattleState.Ready, Define.BattleState.Resolve} },
        { Define.BattleState.Resolve, new HashSet<Define.BattleState>{ Define.BattleState.Wait} },
        { Define.BattleState.Finish,     new HashSet<Define.BattleState>{ Define.BattleState.Wait} }
    };

    public bool TryTransitionTo(Define.BattleState state) {
        if (!CanTransitionTo(state)) {
            Debug.Log($"<color=yellow>{state}로 battle state 변경 불가</color>");
            return false;
        }

        CurrentState = state;
        return true;
    }


    private bool CanTransitionTo(Define.BattleState state) {
        return validTransitions.TryGetValue(state, out var validStates) && validStates.Contains(CurrentState);
    }
}
