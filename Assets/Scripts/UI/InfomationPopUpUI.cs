using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUpData
{
    public string Title => title;
    public string Description => description;
    public IReadOnlyList<UnityAction> ButtonActions => buttonActions;

    [SerializeField] protected string title;
    [SerializeField] protected string description;
    private UnityAction[] buttonActions;

    public PopUpData(string title, string description, UnityAction[] buttonActions) {
        this.title = title;
        this.description = description;
    }
}
public class InfomationPopUpData : PopUpData
{
    public string SubText => subText;
    public Sprite Image => image;

    [SerializeField] protected string subText;
    [SerializeField] protected Sprite image;

    public InfomationPopUpData(string mainText, string description, UnityAction[] buttonActions, string subText=null, Sprite image=null) : base(mainText, description, buttonActions) {
        this.subText = subText;
        this.image = image;
    }
}

public class InfomationPopUpUI : UIBase
{
    [SerializeField] Image image;
    [SerializeField] TitleText mainText;
    [SerializeField] TextMeshProUGUI subText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] CustomButtonContainer buttonContainer;

    public void InitUI(InfomationPopUpData popUpData) {
        if (popUpData.Image != null) {
            image.sprite = popUpData.Image;
            image.gameObject.SetActive(true);
        }
        else {
            image.gameObject.SetActive(false);
        }

        mainText.SetText(popUpData.Title);

        if (!string.IsNullOrEmpty(popUpData.SubText)) {
            subText.text = popUpData.SubText;
            subText.gameObject.SetActive(false);
        }
        else {
            subText.gameObject.SetActive(true);
        }

        descriptionText.SetText(popUpData.Description);

        buttonContainer.Clear();
        for (int i = 0; i < 2; i++) {
            if (buttonContainer.TryGetObj(i, out CustomButton button)) {
                button.SetButtonAction(popUpData.ButtonActions[i]);
            }
        }
    }

    private void Reset() {
        if (mainText == null) {
            mainText = GetComponentInChildren<TitleText>();
        }
    }
}
