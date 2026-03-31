using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;



public class ConstructionUI : UIBase
{
    [SerializeField] ConstructionContentUIContainer ConstructionContentUIContainer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

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
}
