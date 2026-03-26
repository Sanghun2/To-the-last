using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

// 선택 시작부터 선택 처리 끝 까지 관리
public class SelectionManager
{
    public SelectionButton CurrentSelectedButton => currentSelectedButton;
    public Guid? CurrentButtonGuid => currentSelectedButton?.ButtonGuid;

    private SelectionButton currentSelectedButton;

    private SelectionDataParserContainer dataParserContainer;
    private SelectActionContextBuilderContainer contextBuilderContainer;
    private SelectActionConverterContainer actionConverterContainer;

    public bool TryBuildSelectAction(SelectionSD selectionSD, out ActionData actionData) {
        actionData = null;

        if (!dataParserContainer.TryGet(selectionSD, out var parser)) { LogError($"no parser exist. sd type? {selectionSD.GetType()}"); return false; }
        SelectionDataBase selectionData = parser.Parse(selectionSD);

        if (selectionData == null) { LogError($"selection data null"); return false; }

        if (!contextBuilderContainer.TryGet(selectionData, out SelectActionContextBuilderBase contextBuilder)) { LogError($"get context builder failed"); return false; }
        if (!contextBuilder.TryBuildContext(selectionData, out SelectActionContext actionContext)) { LogError($"context build failed"); return false; }


        if (!actionConverterContainer.TryGet(actionContext, out SelectActionConverter actionConverter)) { LogError($"converter get failed"); return false; }
        if (!actionConverter.TryConvertAction(actionContext, out actionData)) { LogError($"converting action data failed"); return false; }

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

    private void LogError(string message) {
        Debug.LogError($"<color=red>{message}</color>");
    }
}
