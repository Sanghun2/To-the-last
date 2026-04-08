using System;
using UnityEngine;

public abstract class SelectionContextBuilderBase
{
    public abstract bool TryBuildSelectionContext(SelectionRunnerDataBase selectionData, ActionData actionData, out SelectionContextBase selectionContext);
}

public abstract class SelectionContextBuilderBase<TInData, TOutContext> : SelectionContextBuilderBase
    where TInData : SelectionRunnerDataBase
    where TOutContext : SelectionContextBase
{
    public override bool TryBuildSelectionContext(SelectionRunnerDataBase selectionData, ActionData actionData, out SelectionContextBase selectionContext) {
        if (selectionData is TInData convertedData) {
            var result = TryBuildSelectionContext(convertedData, actionData, out TOutContext convertedContext);
            selectionContext = convertedContext;
            return result;
        }

        Debug.LogError($"<color=red>failed to convert ({selectionData.GetType()}) to type ({typeof(TInData)})</color>");
        selectionContext = null;
        return false;
    }
    public abstract bool TryBuildSelectionContext(TInData selectionData, ActionData actionData, out TOutContext selectioContext);
}
