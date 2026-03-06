using System;
using System.Collections.Generic;
using BilliotGames;
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
    protected StatContainer statContainer;
    private int _position;


    public BattleEntity(string entityID) : base(entityID) {

    }

    public event Action<BehaviourState, BehaviourState> OnStateChanged;

    public void InitEntity(IReadOnlyList<StatData> statDataList) {
        CurrentState = BehaviourState.Idle;
        _position = 0;

        statContainer.Clear();
        for (int i = 0; i < statDataList.Count; i++) {
            var statData = statDataList[i];
            string id = statData.Stat.ToID();
            statContainer.RegisterStat(id, new Stat(statData.Value));
        }
    }
    public BattleEntity SetPosition(int position) {
        this._position = position;
        return this;
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
