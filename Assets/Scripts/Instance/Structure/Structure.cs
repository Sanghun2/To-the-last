using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Structure : IValue<float>
{
    public enum StructureState
    {
        Locked,
        Empty,
        Built,
    }

    public bool CanContruct => CurrentState == StructureState.Empty;
    public bool CanDestroy => CurrentState == StructureState.Built;
    public bool IsLocked => CurrentState == StructureState.Locked;
    public StructureContextBase StructureContext => structureContext;
    public string ID => StructureContext.ID;
    public string DisplayText => structureContext.DisplayText;

    public float CurrentValue => currentProgress;
    public float MaxValue => maxProgress;
    public StructureState CurrentState
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
    public int Level => level;


    [SerializeField] StructureContextBase structureContext;
    [SerializeField] int level;
    [SerializeField][HideInInspector] StructureState _currentState;

    [SerializeField][HideInInspector] float currentProgress;
    [SerializeField][HideInInspector] float maxProgress;

    public event Action<StructureState, StructureState> OnStateChanged;
    public event Action<Structure> OnUpgraded;

    public Structure() {
        SetAsDefaultState();
    }

    public void SetStructure(StructureContextBase structureContext) {
        this.structureContext = structureContext;
        //structureID = structureContext.ID;
        CurrentState = StructureState.Built;
    }
    public void ApplyUpgrade(string id, StructureContextBase newContext) {
        level += 1;
        //structureID = id;
        structureContext = newContext;

        OnUpgraded?.Invoke(this);
    }

    public void Unlock() {
        if (IsLocked == false) { Debug.Log("<color=yellow>Lock 상태가 아닌데 unlock 시도</color>"); return; }
        CurrentState = StructureState.Empty;
    }
    public void DestroyStrucure() {
        if (CanDestroy == false) { return; }
        CurrentState = StructureState.Empty;
        structureContext = null;
        //structureID = null;
    }

    private void SetAsDefaultState() {
        //structureID = null;
        currentProgress = 0;
        maxProgress = structureContext == null ? 1 : structureContext.ConstructionTime;
        CurrentState = StructureState.Locked;
    }
}
