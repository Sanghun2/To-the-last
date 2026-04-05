using System;
using UnityEngine;


public abstract class SelectActionConverterBase : ActionConverterBase
{

}

public abstract class SelectActionConverterBase<TSelectActionContext> : SelectActionConverterBase
    where TSelectActionContext : SelectActionContextBase
{
    public override ActionData ConvertAction(ActionContextBase context) {
        if (context is TSelectActionContext selectActionContext) {
            return ConvertAction(selectActionContext);
        }

        Debug.LogError($"<color=red>({context.GetType()})은 select action context로 변환할 수 없음</color>");
        return null;
    }
    public virtual ActionData ConvertAction(TSelectActionContext context) {
        return new ActionData(CreateSelectAction(context));
    }

    protected virtual Action CreateSelectAction(TSelectActionContext context) {
        return () => {
            Debug.Log($"select action executed");
            FocusJob selectionProcess = new FocusJob(
            context.JobDuration,
            onStart: OnSelectionStart,
            onProgress: OnSelectionProgress,
            onComplete: () => OnSelectionComplete(context)).WithBlockScreen();

            Managers.Job.DoFocusJob(selectionProcess, () => Managers.Select.ResetSelectedButton());
        };
    }


    protected virtual void OnSelectionStart() {
        Managers.ScreenBlocker.SetActive(true);
    }
    protected virtual void OnSelectionProgress(float currentValue, float maxValue) {
        Managers.Select.CurrentSelectedButton.UpdateProcessUI(currentValue, maxValue);
    }
    protected virtual void OnSelectionComplete(TSelectActionContext context) {
        Debug.Log($"action applied");
        SelectAction(context)?.Invoke();
        Managers.ScreenBlocker.SetActive(false);
    }


    protected abstract Action SelectAction(TSelectActionContext context);
}