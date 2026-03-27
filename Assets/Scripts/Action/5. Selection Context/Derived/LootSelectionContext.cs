using UnityEngine;

public class LootSelectionContext : SelectionContextBase<LootSelectionData>
{
    public LootSelectionContext(LootSelectionData selectionData, ActionData actionData) {
        this.selectionData = selectionData;
        this.actionData = actionData;
    }
}
