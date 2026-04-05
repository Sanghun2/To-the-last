using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentUIBase : UIBase, IPool
{
    public virtual bool IsActive => IsOpened;
    public ExecutionButton ExecutionButton => executionButton;
    public ProgressBarUI ProgressBarUI => progressBarUI;

    [SerializeField] protected Image contentImage;
    [SerializeField] protected ExecutionButton executionButton;
    [SerializeField] protected ProgressBarUI progressBarUI;
    [SerializeField] protected UILocker uiLocker;

    public abstract void InitContent(ContentSDBase contentSD);

    #region Progress

    protected virtual void ExecuteButtonAction(int requireMinutes) {
        if (!CanExecute()) { Debug.Log("실행 조건이 맞지 않아 실행 불가"); return; }

        var job = Managers.Job.CreateFocusJob(
            requireMinutes,
            onProgressStart: OnProgressStart,
            onProgress: OnProgress,
            onComplete: OnProgressComplete).WithBlockScreen();

        Managers.Job.DoFocusJob(job, OnProgressRelease);
    }

    protected virtual bool CanExecute() { return true; }
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
    public virtual bool IsLocked => structure == null || contentSD.RequiredLevel > structure.StructureLevel;

    public ActionData DefaultAction => _defaultAction;

    protected ActionData _defaultAction;
    protected TContentSDBase contentSD;
    protected Structure structure;


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

        _defaultAction = new ActionData(
          contentSD.ExecutionButtonText,
          () => ExecuteButtonAction(contentSD.RequireMinutes));

        SetImage(contentSD.Image);
        executionButton.SetExecuteAction(DefaultAction);

        int structureLevel = Managers.Structure.CurrentSelctedStructure?.StructureLevel ?? 0;
        bool @lock = contentSD.RequiredLevel > structureLevel;
        uiLocker.SetLock(@lock);
    }


    public void SetStructure(Structure structure) {
        this.structure = structure;

        var context = structure.StructureContext;
        if (context != null && !IsLocked) {
            var currentState = context.ProcessState;
            UpdateExecutionButton(currentState, currentState);
            context.OnProcessStateChanged -= UpdateExecutionButton;
            context.OnProcessStateChanged += UpdateExecutionButton;
        }
    }
    public void UpdateExecutionButton(Structure.ProcessState currentState, Structure.ProcessState prevState) {
        bool interactable = currentState == Structure.ProcessState.Available;
        executionButton.SetInteractable(interactable);
    }


    protected override void OnProgressStart() {
        var context = structure?.StructureContext;
        if (context != null && !IsLocked) {
            context.ProcessState = Structure.ProcessState.Processing;
        }
    }

    protected override void OnProgressComplete() {
        var context = structure?.StructureContext;
        if (context != null && !IsLocked) {
            context.ProcessState = Structure.ProcessState.Available;
        }
    }

    protected virtual void OnEnable() {
        var context = structure?.StructureContext;
        if (context != null && !IsLocked) {
            context.OnProcessStateChanged -= UpdateExecutionButton;
            context.OnProcessStateChanged += UpdateExecutionButton;
        }
    }

    protected virtual void OnDisable() {
        var context = structure?.StructureContext;
        if (context != null && !IsLocked) {
            context.OnProcessStateChanged -= UpdateExecutionButton;
        }
    }

    private void SetImage(Sprite image) {
        contentImage.sprite = image;
        contentImage.gameObject.SetActive(image != null);
    }
}