using System;
using UnityEngine;


public abstract class SelectActionConverterBase : ActionConverterBase
{

}

public abstract class SelectActionConverterBase<TSelectionRunnerContext> : SelectActionConverterBase
    where TSelectionRunnerContext : SelectionRunnerContextBase
{
    public override ActionData ConvertAction(SelectionRunnerContextBase runnerContext) {
        if (runnerContext is TSelectionRunnerContext selectActionContext) {
            return ConvertAction(selectActionContext);
        }

        Debug.LogError($"<color=red>({runnerContext.GetType()})은 select action context로 변환할 수 없음</color>");
        return null;
    }
    public virtual ActionData ConvertAction(TSelectionRunnerContext context) {
        return new ActionData(CreateSelectAction(context));
    }

    protected virtual Action CreateSelectAction(TSelectionRunnerContext context) {
        return () => {
            FocusJob selectionProcess = new FocusJob(
            context.JobDuration,
            onStart: OnSelectionStart,
            onProgress: OnSelectionProgress,
            onComplete: () => OnSelectionComplete(context)).WithBlockScreen();

            Managers.Job.DoFocusJob(selectionProcess, () => Managers.Select.ResetSelectedButton());
        };
    }


    protected virtual void OnSelectionStart() {
    }
    protected virtual void OnSelectionProgress(float currentValue, float maxValue) {
        Managers.Select.CurrentSelectedButton.UpdateProcessUI(currentValue, maxValue);
    }
    protected virtual void OnSelectionComplete(TSelectionRunnerContext context) {
        Debug.Log($"action applied");
        SelectAction(context)?.Invoke();
    }


    protected abstract Action SelectAction(TSelectionRunnerContext context);
}