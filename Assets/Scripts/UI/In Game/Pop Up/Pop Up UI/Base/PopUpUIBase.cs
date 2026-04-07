using System;
using System.Collections.Generic;
using System.Xml;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PopUpUIBase : UIBase 
{
    [SerializeField] protected TextUI titleText;
    [SerializeField] protected TextMeshProUGUI subText;
    [SerializeField] protected Image iconImage;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    [SerializeField] protected RequirementUIContainer requirementUIContainer;
    [SerializeField] protected CustomButtonContainer buttonContainer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        buttonContainer.InitUI();

        _isInit = true;
    }

    public virtual void InitPopUp(PopUpDataBase popUpData) {
        if (popUpData == null) { Debug.LogError($"<color=red>pop up data null</color>"); return; }

        InitUI();

        InitPopUpContents(popUpData);
    }

    protected virtual void InitPopUpContents(PopUpDataBase popUpData) {
        SetTitle(popUpData as ITitleContent);
        SetSubText(popUpData as ISubTextContent);
        SetIconImage(popUpData as IImageContent);
        SetDescription(popUpData as IDescriptionContent);
        SetRequirements(popUpData as IRequirementContent);
        SetButtonActions(popUpData.ButtonActions);
    }

    protected virtual void Reset() {
        if (titleText == null) {
            titleText = GetComponentInChildren<TextUI>();
        }
    }

    #region Content

    protected virtual void SetTitle(ITitleContent titleContent) {
        if (titleText == null) return;
        titleText.gameObject.SetActive(titleContent != null);
        titleText.SetText(titleContent?.Title);
    }
    protected virtual void SetDescription(IDescriptionContent descriptionContent) {
        if (descriptionText == null) return;
        descriptionText.gameObject.SetActive(descriptionContent != null);
        descriptionText.SetText(descriptionContent?.Description);
    }
    protected virtual void SetIconImage(IImageContent imageContent) {
        if (iconImage == null) return;
        iconImage.gameObject.SetActive(imageContent != null);
        iconImage.sprite = imageContent?.Image;
    }
    protected virtual void SetSubText(ISubTextContent subTextContent) {
        if (subText == null) return;
        subText.gameObject.SetActive(subTextContent != null);
        subText.text = subTextContent?.SubText;
    }
    protected virtual void SetRequirements(IRequirementContent requirementContent) {
        if (requirementUIContainer == null) return;
        requirementUIContainer.gameObject.SetActive(requirementContent != null);
        requirementUIContainer.ShowRequirements(requirementContent?.Requirements);
    }
    protected virtual void SetButtonActions(IReadOnlyList<ActionData> buttonActions) {
        if (buttonContainer == null) return;
        buttonContainer.Clear();
        int buttonCount = Mathf.Min(2, buttonActions.Count);
        for (int i = 0; i < buttonCount; i++) {
            CustomButton button = buttonContainer.GetOrCreateObjOf(i);
            button.InitButton(buttonActions[i]);
            button.Activate();
        }
    }

    #endregion
}

public abstract class PopUpUIBase<TPopUpData> : PopUpUIBase
    where TPopUpData : PopUpDataBase
{
    public override void InitPopUp(PopUpDataBase popUpData) {
        if (popUpData is TPopUpData data) {
            InitPopUp(data);
        }
    }

    public virtual void InitPopUp(TPopUpData popUpData) {
        if (popUpData == null) { Debug.LogError($"<color=red>pop up data is null</color>"); return; }
        InitUI();
        InitPopUpContents(popUpData);
    }
}
