using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;



public class ConstructionUI : UIBase
{
    [SerializeField] BuildContentUIContainer buildContentUIContainer;
    [SerializeField] ProgressBarUI progressBarUI;

    public override void InitUI() {
        if (IsInit) return;

        progressBarUI.InitUI(0, 1);

        _isInit = true;
    }

    public void ShowConstructionList(IReadOnlyList<StructureSD> structureList) {
        buildContentUIContainer.ShowList(structureList);
    }

    public void UpdateProgressBar(float currentValue, float maxValue) {
        progressBarUI.UpdateUI(currentValue, maxValue);
    }
}
