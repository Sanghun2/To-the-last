using System;
using UnityEngine;

public class SelectActionContext : ActionContext
{
    public SelectionSD SelectionSD => selectionSD;
    public int JobDuration => jobDuration;

    [SerializeField] SelectionSD selectionSD;
    [SerializeField] int jobDuration;

    public SelectActionContext(SelectionSD selectionSD, int jobDuration) {
        this.selectionSD = selectionSD;
        this.jobDuration = jobDuration;
    }   
}
public abstract class SelectActionProcessor : ActionProcessor
{
    public override bool TryGenerateAction(ActionContext context, out ActionData actionData) {
        if (context is SelectActionContext selectActionContext) {
            return TryGenerateAction(selectActionContext.SelectionSD, selectActionContext, out actionData);
        }

        Debug.LogError($"<color=red>({context.GetType()})은 select action context로 변환할 수 없음</color>");
        actionData = null;
        return false;
    }
    public abstract bool TryGenerateAction(SelectionSD selectionSD, SelectActionContext context, out ActionData actionData);
    public abstract bool TryProcess(SelectionSD selectionSD, SelectionContext selectionContext);
}
