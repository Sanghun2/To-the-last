using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

public class Structure : IValue
{
    public enum BuildState {
        Building,
        Built,
        Destroying,
        Destroyed
    }
    public enum InteractionState {
        Idle,
        Interacting, // 제작 중 or 상호작용 중
    }

    public float CurrentValue => currentProgress;
    public float MaxValue => maxProgress;
    public BuildState CurrentState => currentBuildState;
    public InteractionState CurrentInteractionState => currentInteractionState;


    [SerializeField] StructureSD structureSD;
    private BuildState currentBuildState;
    private InteractionState currentInteractionState;


    private float currentProgress;
    private float maxProgress; 


    public Structure(StructureSD structureSD) {
        this.structureSD = structureSD;
        currentBuildState = BuildState.Building;
        currentProgress = 0;
        maxProgress = structureSD.ConstructionTime;
    }
}
