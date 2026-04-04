using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class InfomationPopUpUI : PopUpUIBase
{
    [SerializeField] TouchClosePanel closePanel;

    public override void InitPopUp(PopUpDataBase popUpData) {
        base.InitPopUp(popUpData);

        closePanel.SetCloseAction(popUpData.OnCloseByPanel);
    }

    public void SetSubText(string text) {
        var obj = subText.gameObject;
        if (!obj.activeSelf) obj.SetActive(true);
        subText.text = text;
    }
}
