using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[Serializable]
public class BattleEntity : Entity  
{
    public enum BehaviourState {
        Idle,
        Selected,
    }
    public enum Type {
        Player,
        Enemy,
    }

    public BehaviourState CurrentState
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
    public int Position => _position;

    private BehaviourState _currentState;
    protected StatContainer statContainer = new StatContainer();
    private int _position;


    public BattleEntity(string entityID) : base(entityID) {

    }

    public event Action<BehaviourState, BehaviourState> OnStateChanged;

    #region Init

    public BattleEntity InitEntity(IReadOnlyList<StatData> statDataList) {
        CurrentState = BehaviourState.Idle;
        _position = 0;

        statContainer.ClearStats();
        for (int i = 0; i < statDataList.Count; i++) {
            var statData = statDataList[i];
            string id = statData.Stat.ToID();
            statContainer.RegisterStat(id, new Stat(statData.Value));
        }

        return this;
    }
    public BattleEntity InitEntity(StatContainer statContainer) {
        CurrentState = BehaviourState.Idle;
        _position = 0;
        this.statContainer = statContainer;
        return this;
    }

    public BattleEntity SetPosition(int position) {
        this._position = position;
        return this;
    }

    #endregion


    #region Behaviour

    public bool CanSelectBehaviour() {
        return CurrentState == BehaviourState.Idle;
    }

    #endregion

    #region State

    public int CalculateDistance(BattleEntity targetEntity) {
        return Mathf.Abs(Position - targetEntity.Position);
    }

    public bool TryGetStatValue(Define.Stat statType, out float stat) {
        stat = 0;
        if (statContainer == null) return false;

        Value<float>? statValue = statContainer.GetStatRawValue(statType.ToID());
        if (statValue == null) { Debug.LogError($"<color=red>{statType}에 해당하는 stat이 없음</color>"); return false; }

        Value<float> value = (Value<float>)statValue;
        stat = value.CurrentValue;
        return true;
    }    
    public bool TryChangeStat(string statID, float deltaValue) {
        return statContainer.TryChangeRawStat(statID, deltaValue);
    }
    public bool TryGetStat(string statID, out Stat stat) {
        return statContainer.TryGetStat(statID, out stat);
    }

    public void ResetState(Define.BattleState currentState, Define.BattleState _) {
        switch (currentState) {
            case Define.BattleState.None:
                break;
            case Define.BattleState.Ready:
                break;
            case Define.BattleState.Wait:
                CurrentState = BehaviourState.Idle;
                break;
            case Define.BattleState.Resolve:
                break;
            case Define.BattleState.Finish:
                break;
            default:
                break;
        }
    }

    #endregion
}
