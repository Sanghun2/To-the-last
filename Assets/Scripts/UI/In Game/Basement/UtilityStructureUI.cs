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
    public override void SetUpUI(Structure structure) {
        InitUI();

        var structureContext = structure.StructureContext as UtilityStructureContext;
        if (structureContext != null) {
            SetTitleText(structureContext.DisplayText);
            var contents = structureContext.ContentList;
            ShowContents(contents);
        }
        else {
            Debug.LogError($"({structure.StructureContext}) is not type of ({typeof(UtilityStructureContext)})");
        }
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
