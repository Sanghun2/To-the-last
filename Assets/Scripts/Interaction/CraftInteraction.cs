using UnityEngine;

public class CraftInteraction : IInteract
{
    [SerializeReference] CraftService craftService;

    public bool CanInteract() {
        return true;
    }

    public void Interact() {
        craftService.Execute();
    }
}
