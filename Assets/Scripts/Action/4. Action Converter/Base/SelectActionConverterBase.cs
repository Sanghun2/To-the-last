using System;
using UnityEngine;


public abstract class SelectActionConverterBase : ActionConverterBase
{

}

public abstract class SelectActionConverterBase<TSelectActionContext> : SelectActionConverterBase
    where TSelectActionContext : SelectActionContextBase
{
    public override bool TryConvertAction(ActionContextBase context, out ActionData actionData) {
        if (context is TSelectActionContext selectActionContext) {
            return TryConvertAction(selectActionContext, out actionData);
        }

        Debug.LogError($"<color=red>({context.GetType()})은 select action context로 변환할 수 없음</color>");
        actionData = null;
        return false;
    }
    public virtual bool TryConvertAction(TSelectActionContext context, out ActionData actionData) {
        actionData = new ActionData(ExecuteSelectionProcess(context));
        return true;
    }

    protected virtual Action ExecuteSelectionProcess(TSelectActionContext context) {
        return () => {
            FocusJob selectionProcess = new FocusJob(
            context.JobDuration,
            onProgressChanged: Managers.Select.CurrentSelectedButton.UpdateProcessUI,
            onComplete: ExecuteAction(context));

            Managers.Job.DoFocusJob(selectionProcess, () => Managers.Select.ResetSelectedButton());
        };
    }
    protected abstract Action ExecuteAction(TSelectActionContext context);
}