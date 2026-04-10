using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogUIBase : UIBase
{
    public enum PageState {
        Waiting,
        InProgress,
        Completed,
    }

    public PageState CurrentPageState
    {
        get => _currentPageState;
        protected set
        {
            _currentPageState = value;
        }
    }

    [SerializeField] protected Image talkerImage;
    [SerializeField] protected TextUI characterNameText;
    [SerializeField] protected TextMeshProUGUI dialogText;
    [SerializeField] protected SelectionButtonContainer selectionButtonContainer;
    protected Dialog dialog;
    protected PageState _currentPageState;

    protected virtual void Reset() {
        if (selectionButtonContainer == null) {
            selectionButtonContainer = GetComponentInChildren<SelectionButtonContainer>();
        }
    }

    public void StartDialog(Dialog dialog) {
        InitUI();
        InitBook(dialog);
        UpdatePage();
        Managers.UI.OpenUI(this);
    }
    public void UpdatePage() {
        UpdatePage(dialog.CurrentProgress, dialog.CurrentPage);
    }
    public void UpdatePage(int currentProgress, DialogPageData pageData) {        
        InitPage(pageData);
        AnimatePage();
    }
    public abstract void SkipDialogAnimation();

    protected abstract void AnimatePage();
    protected virtual void InitBook(Dialog dialog) {
        this.dialog = dialog;
    }
    protected virtual void InitPage(DialogPageData dialogPageData) {        
        SetTalkerImage(dialogPageData.TalkerImage);
        SetTalkerNameText(dialogPageData.TalkerName);
        SetDescription(dialogPageData.Description);
        ShowSelections(dialogPageData.Selections);
    }

    protected void SetDescription(string description) {
        dialogText.SetText(description);
    }
    protected void SetTalkerNameText(string characterName) {
        characterNameText.SetText(characterName);
        characterNameText.gameObject.SetActive(!string.IsNullOrEmpty(characterName));
    }
    protected void SetTalkerImage(Sprite talkerImage) {
        this.talkerImage.sprite = talkerImage;
        this.talkerImage.gameObject.SetActive(talkerImage != null);
    }


    protected void ShowSelections(IReadOnlyList<SelectionContext> selections) {
        selectionButtonContainer.Clear();
        var container = selectionButtonContainer;
        for (int i = 0; i < selections.Count; i++) {
            var selectActionData = selections[i];
            var button = container.GetOrCreateObjOf(i);
            button.InitButton(selectActionData);
        }
    }

}
