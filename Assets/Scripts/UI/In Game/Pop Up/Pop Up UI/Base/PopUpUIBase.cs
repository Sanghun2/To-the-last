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
        InitUI();

        if (popUpData == null) {
            Debug.LogError($"<color=red>pop up data null</color>");
            return;
        }

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
        titleText.gameObject.SetActive(titleContent != null);
        titleText.SetText(titleContent?.Title);
    }
    protected virtual void SetDescription(IDescriptionContent descriptionContent) {
        descriptionText.gameObject.SetActive(descriptionContent != null);
        descriptionText.SetText(descriptionContent?.Description);
    }
    protected virtual void SetIconImage(IImageContent imageContent) {
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
        buttonContainer.Clear();
        int buttonCount = Mathf.Min(2, buttonActions.Count);
        for (int i = 0; i < buttonCount; i++) {
            CustomButton button = buttonContainer.GetOrCreateObj(i);
            button.InitButton(buttonActions[i]);
            button.Activate();
        }
    }

    #endregion
}
