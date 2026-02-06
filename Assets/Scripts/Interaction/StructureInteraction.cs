using UnityEngine;

public class StructureInteraction : IInteract
{
    [SerializeField] StructureSD structureSD;

    public void SetStructureSD(StructureSD structureSD) {
        this.structureSD = structureSD;
    }

    public bool CanInteract() {
        throw new System.NotImplementedException();
    }

    public void Interact() {
        throw new System.NotImplementedException();
    }
}
