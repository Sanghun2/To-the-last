using UnityEngine;

public class DialogSelectionRunnerContextBuilder : SelectionRunnerContextBuilderBase<DialogSelectionRunnerData, DialogSelectionRunnerContext>
{
    public override DialogSelectionRunnerContext BuildActionContext(DialogSelectionRunnerData data) {
        return new DialogSelectionRunnerContext(data.DialogID, 0);
    }
}
