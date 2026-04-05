using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

// 선택 시작부터 선택 처리 끝 까지 관리
public class SelectionManager
{
    public SelectionButton CurrentSelectedButton => currentSelectedButton;

    private SelectionDataParserContainer dataParserContainer = new SelectionDataParserContainer();
    private SelectActionContextBuilderContainer contextBuilderContainer = new SelectActionContextBuilderContainer();
    private SelectActionConverterContainer actionConverterContainer = new SelectActionConverterContainer();
    private SelectionContextBuilderContainer selectionContextBuilderContainer = new SelectionContextBuilderContainer();

    private SelectionButton currentSelectedButton;

    #region 확정

    public bool TryBuildSelectionContext(SelectionSD selectionSD, out SelectionContextBase selectionContext) {
        selectionContext = null;

        // SD -> Data
        if (!dataParserContainer.TryGet(selectionSD, out var parser)) { LogError($"no parser exist. sd type? {selectionSD.GetType()}"); return false; }
        SelectionDataBase selectionData = parser.Parse(selectionSD);

        return TryBuildSelectionContext(selectionData, out selectionContext);
    }
    public bool TryBuildSelectionContext(SelectionDataBase selectionData, out SelectionContextBase selectionContext) {
        selectionContext = null;
        if (selectionData == null) { LogError($"selection data null"); return false; }

        // Data -> Action Context
        if (!contextBuilderContainer.TryGet(selectionData, out SelectActionContextBuilderBase actionContextBuilder)) { LogError($"get context builder failed"); return false; }
        if (!actionContextBuilder.TryBuildActionContext(selectionData, out SelectActionContextBase actionContext)) { LogError($"context build failed"); return false; }

        // Action Context -> ActionData
        if (!actionConverterContainer.TryGet(actionContext, out SelectActionConverterBase actionConverter)) { LogError($"converter get failed"); return false; }
        var actionData = actionConverter.ConvertAction(actionContext);

        // Selection Data + ActionData -> Selection Context
        if (!selectionContextBuilderContainer.TryGet(selectionData, out SelectionContextBuilderBase selectionContextBuilder)) { LogError($"selection context builder is not exist"); return false; }
        if (!selectionContextBuilder.TryBuildSelectionContext(selectionData, actionData, out selectionContext)) { LogError($"selection context build failed"); return false; }

        return true;
    }

    public void SetButton(SelectionButton selectionButton) {
        currentSelectedButton = selectionButton;
        Debug.Log($"button set");
    }

    public void ResetSelectedButton() {
        currentSelectedButton.Clear();
        currentSelectedButton = null;
    }

    #endregion

    private void LogError(string message) {
        Debug.LogError($"<color=red>{message}</color>");
    }
}
