using System;
using UnityEngine;


public abstract class SelectActionConverter : ActionConverter
{
    public override bool TryConvertAction(ActionContextBase context, out ActionData actionData) {
        if (context is SelectActionContext selectActionContext) {
            return TryConvertAction(selectActionContext, out actionData);
        }

        Debug.LogError($"<color=red>({context.GetType()})은 select action context로 변환할 수 없음</color>");
        actionData = null;
        return false;
    }
    public abstract bool TryConvertAction(SelectActionContext context, out ActionData actionData);
    public abstract bool TryProcess(SelectionContextBase selectionContext);
}
