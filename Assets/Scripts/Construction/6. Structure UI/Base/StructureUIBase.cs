using System;
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
}
