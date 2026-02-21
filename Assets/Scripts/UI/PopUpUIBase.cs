using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class PopUpData
{
    public string Title => title;
    public string Description => description;
    public IReadOnlyList<ActionData> ButtonActions => buttonActions;

    [SerializeField] protected string title;
    [SerializeField] protected string description;
    protected ActionData[] buttonActions;

    public PopUpData(string title, string description, ActionData[] buttonActions) {
        this.title = title;
        this.description = description;
        this.buttonActions = buttonActions;
    }
}

public abstract class PopUpUIBase<TPopUpData> : UIBase where TPopUpData : PopUpData
{
    [SerializeField] protected TextUI titleText;
    [SerializeField] protected TextUI descriptionText;
    [SerializeField] protected CustomButtonContainer buttonContainer;

    public override void InitUI() {
        if (IsInit) return;

        buttonContainer.InitUI();

        _isInit = true;
    }

    public virtual void InitPopUp(TPopUpData popUpData) {
        InitUI();

        if (popUpData == null) {
            Debug.LogError($"<color=red>pop up data null</color>");
            return;
        }

        titleText.SetText(popUpData.Title);
        descriptionText.SetText(popUpData.Description);

        buttonContainer.Clear();
        int buttonCount = Mathf.Min(2, popUpData.ButtonActions.Count);
        for (int i = 0; i < buttonCount; i++) {
            CustomButton button = buttonContainer.GetObj(i);
            button.InitButton(popUpData.ButtonActions[i]);
            button.Activate();
        }
    }

    protected virtual void Reset() {
        if (titleText == null) {
            titleText = GetComponentInChildren<TextUI>();
        }

        if (descriptionText == null) {
            descriptionText = GetComponentInChildren<TextUI>();
        }
    }
}
