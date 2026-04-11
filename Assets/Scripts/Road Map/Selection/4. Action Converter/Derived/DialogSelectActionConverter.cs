using System;
using UnityEngine;

public class DialogSelectActionConverter : SelectActionConverterBase<DialogSelectionRunnerContext>
{
    protected override Action SelectAction(DialogSelectionRunnerContext context) {
        return () => {
            if (Managers.Dialog.TryStartDialog(context.DialogID, out Dialog startedDialog)) {
                
            }
        };
    }
}
