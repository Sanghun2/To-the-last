using System;
using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Structure : IValue<float>
{
    public enum StructureState {
        Locked,
        Empty,
        Built,
    }

    public bool CanContruct => CurrentState == StructureState.Empty;
    public bool CanDestroy => CurrentState == StructureState.Built;
    public bool IsLocked => CurrentState == StructureState.Locked;
    public StructureContextBase StructureContext => structureContext;

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


    [SerializeField] StructureContextBase structureContext;
    [SerializeField][HideInInspector] string structureID;
    [SerializeField][HideInInspector] StructureState _currentState;
    public event Action<StructureState, StructureState> OnStateChanged;

    [SerializeField][HideInInspector] float currentProgress;
    [SerializeField][HideInInspector] float maxProgress; 


    public Structure() {
        SetAsDefaultState();
    }

    public void ConstructStructure(StructureContextBase structureContext) {
        this.structureContext = structureContext;
        structureID = structureContext.ID;
        CurrentState = StructureState.Built;
    }

    public void Unlock() {
        if (IsLocked == false) { Debug.Log("<color=yellow>Lock 상태가 아닌데 unlock 시도</color>"); return; }
        CurrentState = StructureState.Empty;
    }
    public void DestroyStrucure() {
        if (CanDestroy == false) { return; }
        CurrentState = StructureState.Empty;
        structureContext = null;
        structureID = null;
    }

    private void SetAsDefaultState() {
        structureID = null;
        currentProgress = 0;
        maxProgress = structureContext == null ? 1 : structureContext.ConstructionTime;
        CurrentState = StructureState.Locked;
    }
}
