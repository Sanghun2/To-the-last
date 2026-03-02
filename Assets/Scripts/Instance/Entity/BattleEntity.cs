using System;
using UnityEngine;

public class BattleEntity : Entity
{
    public enum BehaviourState {
        Idle,
        Selected,
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
    public int Position => _position;

    private BehaviourState _currentState;
    private int _position;


    public BattleEntity(string entityID) : base(entityID) {

    }

    public event Action<BehaviourState, BehaviourState> OnStateChanged;

    public void InitEntity() {
        CurrentState = BehaviourState.Idle;
        _position = 0;
    }
    public void SetPosition(int position) {
        this._position = position;
    }
    public bool CanSelectBehaviour() {
        return CurrentState == BehaviourState.Idle;
    }
    public void SelectBehaviour() {
        CurrentState = BehaviourState.Selected;
    }
    public void ResolveBehaviour() {
        CurrentState = BehaviourState.Idle;
    }

    public int CalculateDistance(BattleEntity targetEntity) {
        return Mathf.Abs(Position - targetEntity.Position);
    }
}
