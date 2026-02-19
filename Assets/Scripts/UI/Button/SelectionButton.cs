using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class SelectionButton : ButtonBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] RequirementUI requirementUI;

    public void Init() {
        if (IsInit) return;

        requirementUI.CloseUI();

        _isInit = true;
    }
    public void Activate() {
        OpenUI();
    }
    public void Release() {
        CloseUI();
    }

    public void InitButton(string text, Action buttonAction, Ingredient ingredient=null) {
        Init();
        buttonText.text = text;
        SetButtonAction(buttonAction);

        // requirement가 필요하면 open ui & init
        //requirementUI.Init(ingredient);
    }

    protected override void ButtonAction() {

    }

    protected override void Reset() {
        base.Reset();

        if (buttonText == null) {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (requirementUI == null) {
            requirementUI = GetComponentInChildren<RequirementUI>();
        }
    }
}
