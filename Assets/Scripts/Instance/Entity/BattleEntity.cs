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
    public Define.VitalState CurrentVitalState
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
    public StateBase CurrentState
    {
        get => _currentState;
        set
        {
            var prevState = _currentState;
            _currentState = value;
            if (_currentState != prevState) {
                prevState?.ExitState();
                _currentState?.EnterState();
                OnStateChanged?.Invoke(_currentState, prevState);
            }
        }
    }


    public int Position => _position;

    protected StatContainer statContainer = new StatContainer();
    private StateBase _currentState;
    private BehaviourState _currentBehaviourState;
    private Define.VitalState _currentVitalState;
    private int _position;


    public BattleEntity(string entityID) : base(entityID) {

    }

    public event Action<BehaviourState, BehaviourState> OnBehaviourStateChanged;
    public event Action<Define.VitalState, Define.VitalState> OnVitalStateChanged;
    public event Action<int> OnDistanceChanged;
    public event Action<StateBase, StateBase> OnStateChanged;

    #region Init

    public BattleEntity InitEntity(IReadOnlyList<StatData> statDataList) {
        CurrentBehaviourState = BehaviourState.Idle;
        _position = 0;

        statContainer.CreateDefaultStats();
        statContainer.InitStats(statDataList);

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
        if (CurrentVitalState == Define.VitalState.Alive && value.CurrentValue <= 0) {
            CurrentVitalState = Define.VitalState.Dead;
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
            case Define.BattleState.Exit:
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
