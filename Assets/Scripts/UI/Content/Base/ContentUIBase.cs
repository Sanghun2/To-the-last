using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentUIBase : UIBase, IPool
{
    public virtual bool IsActive => IsOpened;

    [SerializeField] protected Image contentImage;
    [SerializeField] protected ExecutionButton executionButton;
    [SerializeField] protected ProgressBarUI progressBarUI;
    [SerializeField] protected UILocker uiLocker;

    public abstract void InitContent(ContentSDBase contentSD);

    #region Progress

    protected virtual void ExecuteButtonAction(int requireMinutes) {
        var job = Managers.Job.CreateFocusJob(
            requireMinutes,
            onProgressStart: OnProgressStart,
            onProgress: OnProgress,
            onComplete: OnProgressComplete).WithBlockScreen();

        Managers.Job.DoFocusJob(job, OnProgressRelease);
    }

    protected virtual void OnProgressStart() { }

    private void OnProgress(float currentValue, float maxValue) {
        progressBarUI.UpdateUI(currentValue, maxValue);
    }

    protected virtual void OnProgressComplete() { }
    protected virtual void OnProgressRelease() {
        progressBarUI.Clear();
    }

    #endregion


    #region Pool

    public virtual void Activate() {
        OpenUI();
    }
    public virtual void Init() {
        InitUI();
    }
    public virtual void Return() {
        CloseUI();
        progressBarUI.Clear();
    }

    #endregion
}

public abstract class ContentUIBase<TContentSDBase> : ContentUIBase
    where TContentSDBase : ContentSDBase
{
    protected TContentSDBase contentSD;

    public override void InitContent(ContentSDBase contentSDBase) {
        if (contentSDBase is TContentSDBase contentSD) {
            InitContent(contentSD);
            return;
        }

        Debug.LogError($"<color=red>({contentSDBase.GetType()}) is not type of ({typeof(TContentSDBase)})</color>");
    }

    public virtual void InitContent(TContentSDBase contentSD) {
        this.contentSD = contentSD;
        progressBarUI.Clear();

        SetImage(contentSD.Image);
        executionButton.SetExecuteAction( new ActionData(
            contentSD.ExecutionButtonText, 
            () => ExecuteButtonAction(contentSD.RequireMinutes)));

        int structureLevel = Managers.Structure.CurrentSelctedStructure?.StructureLevel ?? 0;
        bool @lock = contentSD.RequiredLevel > structureLevel;
        uiLocker.SetLock(@lock);
    }

    private void SetImage(Sprite image) {
        contentImage.sprite = image;
        contentImage.gameObject.SetActive(image != null);
    }
}