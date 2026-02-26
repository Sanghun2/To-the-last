using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class DialogManager : IInitializable
{
    public bool IsInit => _isInit;

    private Dictionary<string, Dialog> availableDialogDict = new Dictionary<string, Dialog>(200);
    private Dictionary<string, Dialog> completedDialogDict = new(200);
    private bool _isInit;


    public void RegisterDialog(DialogSD newDialogSD) {
        RegisterDialog(newDialogSD, Dialog.State.Idle);
    }
    public void CompleteDialog(Dialog targetDialog) {
        availableDialogDict.Remove(targetDialog.DialogID);
        RegisterDialog(targetDialog.DialogSD, Dialog.State.Done);
    }

    public bool TryGetDialog(string dialogID, out Dialog dialog) {
        if (TryGetDialog(dialogID, Dialog.State.Done, out dialog)) {
            return true;
        }
        else if (TryGetDialog(dialogID, Dialog.State.Idle, out dialog)) {
            return true;
        }

        return false;
    }
    public bool TryGetDialog(string dialogID, Dialog.State state, out Dialog dialog) {

        Dictionary<string, Dialog> targetDict = null;

        switch (state) {
            case Dialog.State.Idle:
            case Dialog.State.Running:
                targetDict = availableDialogDict;
                break;
            case Dialog.State.Done:
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


    private Dialog RegisterDialog(DialogSD dialogSD, Dialog.State state) {
        var dialog = new Dialog(dialogSD, state);
        Dictionary<string, Dialog> targetDict = null;
        switch (state) {
            case Dialog.State.Idle:
            case Dialog.State.Running:
                targetDict = availableDialogDict;
                break;
            case Dialog.State.Done:
                targetDict = completedDialogDict;
                break;
            default:
                break;
        }

        if (targetDict == null) {
            Debug.LogError($"({state}) is undefined state");
            return null;
        }

        if (!targetDict.TryAdd(dialogSD.ID, dialog)) {
            Debug.Log($"({dialogSD.ID}) dialog 중복");
        }

        return dialog;
    }
    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }
    public void Release() {
        throw new System.NotImplementedException();
    }
}
