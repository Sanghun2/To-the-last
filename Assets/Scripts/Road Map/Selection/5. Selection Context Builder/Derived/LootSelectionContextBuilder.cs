using UnityEngine;

public class LootSelectionContextBuilder : SelectionContextBuilderBase<SelectionData, LootSelectionContext>
{
    public override bool TryBuildSelectionContext(SelectionData selectionData, ActionData actionData, out LootSelectionContext selectioContext) {
        selectioContext = new LootSelectionContext(selectionData, actionData);
        return true;
    }
}
