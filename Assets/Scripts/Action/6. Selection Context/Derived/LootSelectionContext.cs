using UnityEngine;

public class LootSelectionContext : SelectionContextBase<LootSelectionRunnerData>
{
    public LootSelectionContext(LootSelectionRunnerData selectionData, ActionData actionData) {
        this.selectionData = selectionData;
        this.actionData = actionData;
    }
}
