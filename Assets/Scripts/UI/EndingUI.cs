using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingUI : UIBase
{
    [SerializeField] MainEndingContentUI mainContentUI;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public void ShowUI(string endingID) {
        InitUI();
        mainContentUI.ShowUI(endingID);
        OpenUI();
    }
}
