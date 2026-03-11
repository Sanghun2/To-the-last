using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public abstract class SelectionContext
{

}

//public abstract class SelectionHandler
//{
//    public abstract void ExecuteAsync(SelectionSD selectionSD, SelectionContext context);
//}
//public abstract class SelectionHandler<TSelection, TSelectionContext> : SelectionHandler
//    where TSelection : SelectionSD
//    where TSelectionContext : SelectionContext
//{
//    public override void ExecuteAsync(SelectionSD selectionSD, SelectionContext context) {
//        var convertedContext = context as TSelectionContext;
//        ExecuteAsync((TSelection)selectionSD, convertedContext);
//    }
//    public abstract void ExecuteAsync(TSelection selectionSD, TSelectionContext context = null);
//}

public class SelectActionPipeline
{
    public SelectionButton CurrentSelectedButton => currentSelectedButton;
    public Guid? CurrentButtonGuid => currentSelectedButton?.ButtonGuid;

    private SelectionButton currentSelectedButton;

    public bool TryBuildSelectAction(SelectionSD selectionSD, out ActionData actionData) {
        actionData = null;
        if (!Managers.Registry.SelectAction.TryGetContextGenerator(selectionSD, out SelectActionContextGenerator contextGenerator)) {
            Debug.LogError($"<color=red>({selectionSD.GetType()}) context generator 없음</color>");
            return false;
        }

        if (!contextGenerator.TryGenerateContext(selectionSD, out SelectActionContext context)) {
            Debug.LogError($"<color=red>({selectionSD.GetType()}) context 생성 실패</color>");
            return false;
        }

        if (!Managers.Registry.SelectAction.TryGenerateSelectAction(selectionSD, context, out ActionData selectActionData)) {
            Debug.LogError($"<color=red>({selectionSD.GetType()}) action data 생성 실패</color>");
            return false;
        }

        actionData = selectActionData;
        return true;
    }

    public void SetButton(SelectionButton selectionButton) {
        if (CanSelect()) {
            currentSelectedButton = selectionButton;
        }
        else {
            Debug.Log($"현재 선택 불가능 상태");
        }
    }

    private bool CanSelect() {
        return currentSelectedButton == null && !Managers.Job.IsFocusJobRunning;
    }

    public void ForceClearButton() {
        currentSelectedButton = null;
    }
    public void ClearButton(Guid? buttonID) {
        if (buttonID == null) return;

        if (buttonID.Equals(CurrentButtonGuid)) {
            ForceClearButton();
        }
        else {
            Debug.LogError($"({buttonID})는 현재 버튼({CurrentButtonGuid})이 아님");
        }
    }
}
