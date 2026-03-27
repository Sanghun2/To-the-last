using UnityEngine;

public class LootSelectionContextBuilder : SelectionContextBuilderBase<LootSelectionData, LootSelectionContext>
{
    public override bool TryBuildSelectionContext(LootSelectionData selectionData, ActionData actionData, out LootSelectionContext selectioContext) {
        selectioContext = new LootSelectionContext(selectionData, actionData);
        return true;
    }
}
