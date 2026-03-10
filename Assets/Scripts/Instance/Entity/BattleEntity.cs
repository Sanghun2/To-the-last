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
    public enum VitalState {
        None,
        Alive,
        Dead,
    }

    public BehaviourState CurrentBehaviourState
    {
        get => _currentBehaviourState;
        set
        {
            var prevState = _currentBehaviourState;
            _currentBehaviourState = value;

            if (_currentBehaviourState != prevState) {
                OnBehaviourStateChanged?.Invoke(_currentBehaviourState, prevState);
            }
        }
    }
    public VitalState CurrentVitalState
    {
        get => _currentVitalState;
        protected set
        {
            var prevState = _currentVitalState;
            _currentVitalState = value;
            if (_currentVitalState != prevState) {
                OnVitalStateChanged?.Invoke(_currentVitalState, prevState);
            }
        }
    }

    public int Position => _position;

    private BehaviourState _currentBehaviourState;
    private VitalState _currentVitalState;
    protected StatContainer statContainer = new StatContainer();
    private int _position;


    public BattleEntity(string entityID) : base(entityID) {

    }

    public event Action<BehaviourState, BehaviourState> OnBehaviourStateChanged;
    public event Action<VitalState, VitalState> OnVitalStateChanged;

    #region Init

    public BattleEntity InitEntity(IReadOnlyList<StatData> statDataList) {
        CurrentBehaviourState = BehaviourState.Idle;
        _position = 0;

        statContainer.ClearStats();
        for (int i = 0; i < statDataList.Count; i++) {
            var statData = statDataList[i];
            string id = statData.Stat.ToID();
            statContainer.RegisterStat(id, new Stat(statData.Value));
        }

        string hpID = Define.Stat.Hp.ToID();
        statContainer.UnregisterEvent(hpID, UpdateVital);
        statContainer.RegisterEvent(hpID, UpdateVital);

        return this;
    }

    public BattleEntity InitEntity(StatContainer statContainer) {
        CurrentBehaviourState = BehaviourState.Idle;
        _position = 0;
        this.statContainer = statContainer;
        return this;
    }

    public BattleEntity SetPosition(int position) {
        this._position = position;
        return this;
    }



    private void UpdateVital(Value<float> value) {
        if (CurrentVitalState == VitalState.Alive && value.CurrentValue <= 0) {
            CurrentVitalState = VitalState.Dead;
            Debug.Log($"[Test] entity ({EntityID}) is dead");
        }
    }

    #endregion


    #region Behaviour

    public bool CanSelectBehaviour() {
        return CurrentBehaviourState == BehaviourState.Idle;
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
                CurrentBehaviourState = BehaviourState.Idle;
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
