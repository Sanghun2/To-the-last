using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingUI : UIBase
{
    [SerializeField] MainEndingContentUI mainContentUI;

    public void ShowUI() {
        string endingID = "";
        mainContentUI.ShowUI(endingID);
    }
}
