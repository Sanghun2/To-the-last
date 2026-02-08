using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;



public class ConstructionUI : UIBase
{
    [SerializeField] BuildContentUIContainer buildContentUIContainer;
    [SerializeField] ProgressBarUI progressBarUI;

    public void ShowConstructionList(IReadOnlyList<StructureSD> structureList) {
        buildContentUIContainer.ShowList(structureList);
    }

    public void UpdateProgressBar(float currentValue, float maxValue) {
        progressBarUI.UpdateUI(currentValue, maxValue);
    }
}
