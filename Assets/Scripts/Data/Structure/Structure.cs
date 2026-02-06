using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

public class Structure : IValue
{
    public enum BuildState {
        Building,
        Built,
    }
    public enum InteractionState {
        Idle,
        Interacting, // 제작 중 or 상호작용 중
    }

    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;
    public BuildState CurrentState => currentBuildState;
    public InteractionState CurrentInteractionState => currentInteractionState;


    [SerializeField] StructureSD structureSD;
    private BuildState currentBuildState;
    private InteractionState currentInteractionState;


    private float currentValue;
    private float maxValue; 


    public Structure(StructureSD structureSD) {
        this.structureSD = structureSD;
        currentBuildState = BuildState.Building;
        currentValue = 0;
        maxValue = structureSD.BuildingTime;
    }
}
