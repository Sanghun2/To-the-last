using System;
using UnityEngine;

public class BattleEntity : Entity
{
    public enum BehaviourState {
        Idle,
        Selected,
    }

    public BattleEntity(string entityID) : base(entityID) {

    }

    public BehaviourState CurrentState
    {
        get => _currentState;
        protected set
        {
            var prevState = _currentState;
            _currentState = value;

            if (_currentState != prevState) {
                OnStateChanged?.Invoke(_currentState, prevState);
            }
        }
    }

    public event Action<BehaviourState, BehaviourState> OnStateChanged;

    private BehaviourState _currentState;

    public void InitEntity() {
        CurrentState = BehaviourState.Idle;
    }
    public void SelectBehaviour() {
        CurrentState = BehaviourState.Selected;
    }
    public void ResolveBehaviour() {
        CurrentState = BehaviourState.Idle;
    }
}
