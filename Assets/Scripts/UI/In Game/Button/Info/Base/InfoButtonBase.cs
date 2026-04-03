using System;
using BilliotGames;
using UnityEngine;

public abstract class InfoButtonBase : ButtonBase
{
    protected string dataID;

    public virtual void SetData(string itemID) {
        this.dataID = itemID;
    }

    protected override void ButtonAction() {
        ShowInfomation();
    }

    protected virtual void ShowInfomation() {
        if (string.IsNullOrEmpty(dataID)) { Debug.Log("타겟 아이템 없음"); return; }
        if (!Managers.SD.TryGetSD(dataID, out ItemSD itemSD)) { return; }

        var infoUI = Managers.UI.GetUI<InfomationPopUpUI>();
        if (infoUI.IsOpened) return;

        InfomationPopUpData infoData = new InfomationPopUpData(
            itemSD.DisplayText,
            itemSD.Description,
            new ActionData[] { new ActionData("확인", () => Managers.UI.CloseUI(infoUI)) },
            image: itemSD.Image);
        Managers.UI.OpenUI(infoUI).InitPopUp(infoData);
    }
}
