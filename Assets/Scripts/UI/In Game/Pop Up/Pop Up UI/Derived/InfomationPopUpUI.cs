using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class InfomationPopUpUI : PopUpUIBase<InfomationPopUpData>
{
    public override void InitPopUp(InfomationPopUpData popUpData) {
        base.InitPopUp(popUpData);
    }

    public void SetSubText(string text) {
        var obj = subText.gameObject;
        if (!obj.activeSelf) obj.SetActive(true);
        subText.text = text;
    }
}
