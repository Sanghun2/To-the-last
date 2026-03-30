using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class UtilityStructureUI : StructureUIBase<UtilityStructureContext>
{
    [SerializeField] UtilityContentUIContainer utilityContentUIContainer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public override void SetUpUI(UtilityStructureContext structureContext) {
        InitUI();

        SetTitleText(structureContext.DisplayText);
        var contents = structureContext.ContentList;
        ShowContents(contents);
    }

    private void ShowContents(IReadOnlyList<UtilityContentSD> contents) {
        utilityContentUIContainer.Clear();
        for (int i = 0; i < contents.Count; i++) {
            var content = contents[i];
            var contentUI = utilityContentUIContainer.GetOrCreateObj(i);
            contentUI.InitContent(content);
        }
    }
}
