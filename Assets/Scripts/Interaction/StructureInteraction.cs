using UnityEngine;

public class StructureInteraction : IInteract
{
    [SerializeField] StructureSDBase structureSD;

    public void SetStructureSD(StructureSDBase structureSD) {
        this.structureSD = structureSD;
    }

    public bool CanInteract() {
        throw new System.NotImplementedException();
    }

    public void Interact() {
        throw new System.NotImplementedException();
    }
}
