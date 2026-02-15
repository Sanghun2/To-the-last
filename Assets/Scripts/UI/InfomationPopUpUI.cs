using BilliotGames;
using UnityEngine;
using UnityEngine.UI;


public class InfomationPopUpData : PopUpData
{
    public string SubText => subText;
    public Sprite Image => image;

    [SerializeField] protected string subText;
    [SerializeField] protected Sprite image;

    public InfomationPopUpData(string mainText, string description, ActionData[] buttonActions, string subText = null, Sprite image = null) : base(mainText, description, buttonActions) {
        this.subText = subText;
        this.image = image;
    }
}

public class InfomationPopUpUI : PopUpUIBase<InfomationPopUpData>
{
    [SerializeField] protected Image iconImage;
    [SerializeField] TextUI subText;

    public override void InitUI() {
        if (IsInit) return;
        buttonContainer.InitUI();
        _isInit = true;
    }

    public override void InitPopUp(InfomationPopUpData popUpData) {
        InitPopUp(popUpData);

        if (popUpData.Image != null) {
            iconImage.sprite = popUpData.Image;
            iconImage.gameObject.SetActive(true);
        }
        else {
            iconImage.gameObject.SetActive(false);
        }


        if (!string.IsNullOrEmpty(popUpData.SubText)) {
            subText.SetText(popUpData.SubText);
            subText.gameObject.SetActive(false);
        }
        else {
            subText.gameObject.SetActive(true);
        }
    }
}
