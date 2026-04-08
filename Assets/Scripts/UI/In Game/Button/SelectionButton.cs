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
    private Action buttonAction;


    public void InitButton(SelectActionData actionData) {
        Init();

        SetDescriptionText(actionData.Text);
        buttonAction = actionData.Action;

        SetRequirements(actionData.RequirementType, actionData.Requirement);

        // 선택 불가능한 선택지인 경우 lock on
        SetLock(actionData);
    }
    public void Clear() {
        progressBarUI.Clear();
        buttonAction = null;
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
        buttonAction?.Invoke();
    }


    private void SetRequirements(Define.RequirementType requirementType, Ingredient requirement) {
        // requirement가 필요하면 open ui & init
        if (requirement != null) {
            // requirement의 trait, item에 따라 sprite, text 처리 후 반환
            var count = InventoryUtility.GetItemCount(requirement.ItemSD.ID);
            Debug.LogAssertion($"<color=cyan>trait, item에 따라 처리 후 sprite, text 반환 필요</color>");

            // 반환 된 내용으로 ui init
            Sprite requirementImage = null;
            string requirementText = string.Empty;
            bool isMet = true;
            requirementUI.SetReqirementUI(requirementImage, requirementText, isMet);
        }
        requirementUI.gameObject.SetActive(requirement != null);
    }
    private void SetLock(SelectActionData actionData) {
        lockObj.SetActive(actionData.IsLocked);
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
        buttonAction = null;
    }

    #endregion
}
