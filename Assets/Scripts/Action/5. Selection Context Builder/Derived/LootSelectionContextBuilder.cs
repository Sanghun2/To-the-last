using UnityEngine;

public class LootSelectionContextBuilder : SelectionContextBuilderBase<LootSelectionRunnerData, LootSelectionContext>
{
    public override bool TryBuildSelectionContext(LootSelectionRunnerData selectionData, ActionData actionData, out LootSelectionContext selectioContext) {
        selectioContext = new LootSelectionContext(selectionData, actionData);
        return true;
    }
}
