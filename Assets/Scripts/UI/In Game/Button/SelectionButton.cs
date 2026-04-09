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
    public ProgressBarUI ProgressBarUI => progressBarUI;

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] RequirementUI requirementUI;
    [SerializeField] ProgressBarUI progressBarUI;
    [SerializeField] GameObject lockObj;
    private Action selectAction;
    //private SelectionContext selectionContext;


    public void InitButton(SelectionContext selectionContext) {
        Init();

        SetDescriptionText(selectionContext.Description);
        selectAction = selectionContext.SelectAction;

        SetRequirements(selectionContext.RequirementType, selectionContext.Requirement);

        // 선택 불가능한 선택지인 경우 lock on
        SetLock(selectionContext.IsLocked);
        //this.selectionContext = selectionContext;
    }
    public void Clear() {
        progressBarUI.Clear();
        selectAction = null;
        //selectionContext = null;
    }
    public void UpdateProcessUI(float currentValue, float maxValue) {
        progressBarUI.UpdateUI(currentValue, maxValue);
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
    protected override void ButtonAction() {
        if (Managers.Job.IsFocusJobRunning) { Debug.Log($"already job is runnning"); return; }

        Managers.Select.SetButton(this);
        selectAction?.Invoke();
    }


    private void SetRequirements(Define.RequirementType requirementType, Requirement requirement) {
        // requirement가 필요하면 open ui & init
        if (requirement != null && requirementType != Define.RequirementType.Free) {
            Sprite requirementImage = requirement.Image;
            string requirementText = requirement.Amount.ToString();
            bool isMet = true;
            requirementUI.SetReqirementUI(requirementImage, requirementText, isMet);
        }
        requirementUI.gameObject.SetActive(requirementType != Define.RequirementType.Free);
    }
    private void SetLock(bool isLocked) {
        lockObj.SetActive(isLocked);
    }
    private void SetDescriptionText(string text) {
        buttonText.text = text;
    }


    #region Pool

    public void Init() {
        if (IsInit) return;

        base.InitUI();
        requirementUI.CloseUI();
        progressBarUI.Clear();

        _isInit = true;
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
        selectAction = null;
    }

    #endregion
}
