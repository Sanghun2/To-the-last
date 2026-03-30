using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public abstract class StructureUIBase : UIBase
{
    [SerializeField] TextMeshProUGUI titleText;

    public void SetTitleText(StructureContextBase structureContext) {
        titleText.text = structureContext.Data.DisplayText;
    }
}
