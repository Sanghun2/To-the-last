using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : ButtonBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] TextMeshProUGUI buttonText;
    private ActionData actionData;

    public void Init() {
        InitUI();
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    public void InitButton(ActionData actionData) {
        Init();
        this.actionData = actionData;
        SetButtonText(actionData.Text);
        SetButtonAction(actionData.Action);

        targetButton.interactable = actionData.CanExecute.Invoke();
    }

    protected override void ButtonAction() {
        // actionButton base의 set actionButton action으로 할당해서 사용
    }

    protected override void Reset() {
        base.Reset();
        if (buttonText == null) {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void SetButtonText(string text) {
        if (buttonText == null) { return; }
        buttonText.text = text;
        buttonText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }
}
