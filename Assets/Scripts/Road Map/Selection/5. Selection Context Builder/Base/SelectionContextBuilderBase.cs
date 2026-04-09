using System;
using UnityEngine;

public abstract class SelectionContextBuilderBase
{
    public abstract bool TryBuildSelectionContext(SelectionDataBase selectionData, ActionData actionData, out SelectionContextBase selectionContext);
}

public abstract class SelectionContextBuilderBase<TSelectionData, TSelectionContext> : SelectionContextBuilderBase
    where TSelectionData : SelectionDataBase
    where TSelectionContext : SelectionContextBase
{
    public override bool TryBuildSelectionContext(SelectionDataBase selectionData, ActionData actionData, out SelectionContextBase selectionContext) {
        if (selectionData is TSelectionData convertedData) {
            var result = TryBuildSelectionContext(convertedData, actionData, out TSelectionContext convertedContext);
            selectionContext = convertedContext;
            return result;
        }

        Debug.LogError($"<color=red>failed to convert ({selectionData.GetType()}) to type ({typeof(TSelectionData)})</color>");
        selectionContext = null;
        return false;
    }
    public abstract bool TryBuildSelectionContext(TSelectionData selectionData, ActionData actionData, out TSelectionContext selectioContext);
}
