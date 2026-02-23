using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class SelectionButton : ButtonBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] RequirementUI requirementUI;
    [SerializeField] GameObject lockObj;

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

    public void InitButton(string text, Action buttonAction, bool isLocked = false, Ingredient ingredient=null) {
        Init();
        buttonText.text = text;
        SetButtonAction(buttonAction);

        // requirement가 필요하면 open ui & init
        //requirementUI.Init(requirement);

        // 선택 불가능한 선택지인 경우 lock on
        lockObj.SetActive(isLocked);
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
