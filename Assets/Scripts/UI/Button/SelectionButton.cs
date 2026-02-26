using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    public Guid? ButtonGuid => buttonGuid;

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] RequirementUI requirementUI;
    [SerializeField] Image processImage;
    [SerializeField] GameObject lockObj;
    protected Guid? buttonGuid;

    public void Init() {
        if (IsInit) return;

        requirementUI.CloseUI();
        processImage.fillAmount = 0;
        buttonGuid = Guid.NewGuid();

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
        SetButtonAction(() => {
            Managers.SelectActionPipeline.SetButton(this);
            buttonAction?.Invoke();
        });

        // requirement가 필요하면 open ui & init
        if (context.Requirement != null) {
            requirementUI.SetReqirementItem(context.Requirement);
        }
        requirementUI.gameObject.SetActive(context.Requirement != null);

        // 선택 불가능한 선택지인 경우 lock on
        lockObj.SetActive(context.IsLocked);
    }
    public void UpdateProcessUI(float currentValue, float maxValue) {
        processImage.fillAmount = currentValue / maxValue;
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
