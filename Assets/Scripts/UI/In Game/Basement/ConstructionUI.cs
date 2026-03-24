using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;



public class ConstructionUI : UIBase
{
    [SerializeField] ConstructionContentUIContainer ConstructionContentUIContainer;
    [SerializeField] ProgressBarUI progressBarUI;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        progressBarUI.InitUI(0, 1);

        _isInit = true;
    }

    public void ShowConstructionCatalogs(IReadOnlyList<StructureSD> structureList) {
        if (structureList == null) {
            Debug.LogError($"show construction failed. list null");
            Managers.UI.OpenUI<InfomationPopUpUI>().InitPopUp(new InfomationPopUpData(
                "알림",
                "설치 가능한 구조물이 없습니다.",
                new ActionData[] {
                    new ActionData("확인", () => Managers.UI.CloseUI<InfomationPopUpUI>())
                }));
            return;
        }
        ConstructionContentUIContainer.ShowList(structureList);
    }

    public void SetProgressBar(float currentValue, float maxValue) {
        progressBarUI.InitUI(currentValue, maxValue);
    }
    public void UpdateProgressBar(float currentValue, float maxValue) {
        progressBarUI.UpdateUI(currentValue, maxValue);
    }
}
