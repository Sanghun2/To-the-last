using UnityEngine;

public class LootSelectionContext : SelectionContextBase<SelectionData>
{
    public LootSelectionContext(SelectionData selectionData, ActionData actionData) {
        this.selectionData = selectionData;
        this.actionData = actionData;
    }
}
