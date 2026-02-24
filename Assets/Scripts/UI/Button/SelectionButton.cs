using System;
using BilliotGames;
using TMPro;
using UnityEngine;


public class SelectionButtonContext
{
    public bool IsLocked => isLocked;
    public Ingredient Requirement => requrement;

    [SerializeField] bool isLocked;
    [SerializeField] Ingredient requrement;

    public SelectionButtonContext SetLock(bool @lock) {
        isLocked = @lock;
        return this;
    }
    public SelectionButtonContext SetRequirement(Ingredient requirement) {
        this.requrement = requirement;
        return this;
    }
}

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

    public void InitButton(string text, Action buttonAction, SelectionButtonContext context) {
        Init();
        buttonText.text = text;
        SetButtonAction(buttonAction);

        // requirement가 필요하면 open ui & init
        if (context.Requirement != null) {
            requirementUI.SetReqirementItem(context.Requirement);
        }
        requirementUI.gameObject.SetActive(context.Requirement != null);

        // 선택 불가능한 선택지인 경우 lock on
        lockObj.SetActive(context.IsLocked);
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
