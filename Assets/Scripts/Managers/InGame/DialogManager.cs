using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class DialogManager : IInitializable
{
    public bool IsInit => _isInit;

    public Dialog CurrentDialog => currentDialog;

    private Dictionary<string, Dialog> availableDialogDict = new Dictionary<string, Dialog>(200);
    private Dictionary<string, Dialog> completedDialogDict = new(200);
    private Dialog currentDialog;
    private bool _isInit;

    public event Action<Dialog> OnDialogCompleted;
    public event Action<Dialog> OnDialogStarted;

    public bool TryStartDialog(string dialogBookID, out Dialog startedDialog) {
        startedDialog = null;
        if (!Managers.SD.TryGetSD<DialogBookSD>(dialogBookID, out var dialogBookSD)) { return false; }

        var dialog = new Dialog(new DialogBookData(dialogBookSD));
        currentDialog = dialog;

        // nessasary
        dialog.CurrentState = Dialog.State.InProgress;
        DialogUIBase dialogUIBase = Managers.UI.OpenUI<DialogUI>();
        dialog.OnPageChanged -= dialogUIBase.ShowPage;
        dialog.OnPageChanged += dialogUIBase.ShowPage;
        dialogUIBase.StartDialog(dialog);
        startedDialog = dialog;

        OnDialogStarted?.Invoke(dialog);
        return true;
    }
    public bool TryCompleteCurrentDialog() {
        if (currentDialog == null) { return false; }

        CompleteDialog(currentDialog);
        return true;
    }

    public bool TryGetDialog(string dialogID, out Dialog dialog) {
        if (TryGetDialog(dialogID, Dialog.State.Completed, out dialog)) {
            return true;
        }
        else if (TryGetDialog(dialogID, Dialog.State.Waiting, out dialog)) {
            return true;
        }

        return false;
    }
    public bool TryGetDialog(string dialogID, Dialog.State state, out Dialog dialog) {

        Dictionary<string, Dialog> targetDict = null;

        switch (state) {
            case Dialog.State.Waiting:
            case Dialog.State.InProgress:
                targetDict = availableDialogDict;
                break;
            case Dialog.State.Completed:
                targetDict = completedDialogDict;
                break;
            default:
                break;
        }

        if (targetDict.TryGetValue(dialogID, out dialog)) {
            return true;
        }

        Debug.Log($"({dialogID}) is not available dialog");
        return false;
    }

    private void CompleteDialog(Dialog targetDialog) {
        if (targetDialog == null) return;
        targetDialog.CurrentState = Dialog.State.Completed;

        availableDialogDict.Remove(targetDialog.DialogID);
        completedDialogDict.Add(targetDialog.DialogID, targetDialog);

        currentDialog = null;
        OnDialogCompleted?.Invoke(targetDialog);
    }

    #region Init

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }
    public void Release() {
    }

    #endregion
}
