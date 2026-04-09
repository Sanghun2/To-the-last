using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

// 선택 시작부터 선택 처리 끝 까지 관리
public class SelectionManager
{
    public SelectionButton CurrentSelectedButton => currentSelectedButton;

    private SelectionRunnerDataParserContainer runnerDataParserContainer = new SelectionRunnerDataParserContainer();
    private SelectionRunnerContextBuilderContainer runnerContextBuilderContainer = new SelectionRunnerContextBuilderContainer();
    private SelectActionConverterContainer actionConverterContainer = new SelectActionConverterContainer();
    private SelectionContextBuilderContainer selectionContextBuilderContainer = new SelectionContextBuilderContainer();

    private SelectionButton currentSelectedButton;

    public bool TryBuildSelectionContext(SelectionSDContext selectionSDContext, out SelectionContext selectionContext) {
        selectionContext = null;

        // RunnerSD -> Data
        var selectionData = new SelectionData(selectionSDContext.SelectionSD);
        var selectionRunnerSD = selectionSDContext.SelectionRunnerSD;
        if (!runnerDataParserContainer.TryGet(selectionRunnerSD, out var parser)) { LogError($"no parser exist. sd type? {selectionSDContext.GetType()}"); return false; }
        SelectionRunnerDataBase selectionRunnerData = parser.ParseRunnerData(selectionRunnerSD, selectionData.RequireMinutes);
        
        var selectionBuildContext = new SelectionBuildContext(selectionData, selectionRunnerData);

        return TryBuildSelectionContext(selectionBuildContext, out selectionContext);
    }
    public bool TryBuildSelectionContext(SelectionBuildContext selectionBuildContext, out SelectionContext selectionContext) {
        selectionContext = null;
        if (selectionBuildContext == null) { LogError($"selectionBuildContext is null"); return false; }

        // Data -> Action Context
        var selectionRunnerData = selectionBuildContext.SelectionRunnerDataBase;
        if (!runnerContextBuilderContainer.TryGet(selectionRunnerData, out SelectionRunnerContextBuilderBase selectionRunnerContextBuilder)) { LogError($"get context builder failed"); return false; }
        var selectionRunnerContext = selectionRunnerContextBuilder.BuildSelectionRunnerContext(selectionRunnerData);


        // Action Context -> ActionData
        if (!actionConverterContainer.TryGet(selectionRunnerContext, out SelectActionConverterBase selecActionConverter)) { LogError($"converter get failed"); return false; }
        var selectActionData = selecActionConverter.ConvertAction(selectionRunnerContext);

        // Selection Data + ActionData -> Selection Context
        //if (!selectionContextBuilderContainer.TryGet(selectionRunnerData, out SelectionContextBuilderBase selectionContextBuilder)) { LogError($"selection context builder is not exist"); return false; }
        //if (!selectionContextBuilder.TryBuildSelectionContext(selectionBuildContext.SelectionData, selectActionData, out selectionContext)) { LogError($"selection context build failed"); return false; }

        selectionContext = new SelectionContext(selectionBuildContext.SelectionData, selectActionData);

        return true;
    }

    public void SetButton(SelectionButton selectionButton) {
        currentSelectedButton = selectionButton;
    }

    public void ResetSelectedButton() {
        currentSelectedButton.Clear();
        currentSelectedButton = null;
    }

    private void LogError(string message) {
        Debug.LogError($"<color=red>{message}</color>");
    }
}
