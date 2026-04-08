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

    public bool TryBuildSelectionContext(SelectionPair selectionPair, out SelectionContextBase selectionContext) {
        selectionContext = null;

        // SD -> Data
        var selectionRunnerSD = selectionPair.SelectionRunnerSD;
        if (!dataParserContainer.TryGet(selectionRunnerSD, out var parser)) { LogError($"no parser exist. sd type? {selectionPair.GetType()}"); return false; }
        SelectionRunnerDataBase selectionData = parser.Parse(selectionRunnerSD);

        return TryBuildSelectionContext(selectionData, out selectionContext);
    }
    public bool TryBuildSelectionContext(SelectionRunnerDataBase selectionRunnerData, out SelectionContextBase selectionContext) {
        selectionContext = null;
        if (selectionRunnerData == null) { LogError($"selection data null"); return false; }

        // Data -> Action Context
        if (!contextBuilderContainer.TryGet(selectionRunnerData, out SelectActionContextBuilderBase actionContextBuilder)) { LogError($"get context builder failed"); return false; }

        var actionContext = actionContextBuilder.BuildActionContext(selectionRunnerData);


        // Action Context -> ActionData
        if (!actionConverterContainer.TryGet(actionContext, out SelectActionConverterBase actionConverter)) { LogError($"converter get failed"); return false; }
        var actionData = actionConverter.ConvertAction(actionContext);

        // Selection Data + ActionData -> Selection Context
        if (!selectionContextBuilderContainer.TryGet(selectionRunnerData, out SelectionContextBuilderBase selectionContextBuilder)) { LogError($"selection context builder is not exist"); return false; }
        if (!selectionContextBuilder.TryBuildSelectionContext(selectionRunnerData, actionData, out selectionContext)) { LogError($"selection context build failed"); return false; }

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

    private void LogError(string message) {
        Debug.LogError($"<color=red>{message}</color>");
    }
}
