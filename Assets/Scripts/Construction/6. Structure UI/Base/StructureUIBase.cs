using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;

public abstract class StructureUIBase : UIBase
{
    [SerializeField] protected TextMeshProUGUI titleText;

    public void SetTitleText(string text) {
        titleText.text = text;
    }
}

public abstract class StructureUIBase<TContext> : StructureUIBase
    where TContext : StructureContextBase
{
    public abstract void SetUpUI(Structure structure);

    protected void InitExecutionButtonTexts(string defaultText, IReadOnlyList<ContentSDBase> contents) {
        for (int i = 0; i < contents.Count; i++) {
            var content = contents[i];
            if (string.IsNullOrEmpty(content.ExecutionButtonText)) {
                content.SetDefaultExecutionButtonText(defaultText);
            }
        }
    }
}
