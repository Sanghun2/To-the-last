using System.Collections.Generic;
using UnityEngine;

public class DialogManager : IInitializable
{
    public bool IsInit => _isInit;

    private Dictionary<string, Dialog> availableDialogDict = new Dictionary<string, Dialog>(200);
    private Dictionary<string, Dialog> completedDialogDict = new(200);
    private bool _isInit;


    public Dialog ReigisterDialog(DialogSD dialogSD, Dialog.State state) {
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
    public bool TryGetDialog(string id, Dialog.State state, out Dialog dialog) {

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

        if (targetDict.TryGetValue(id, out dialog)) {
            return true;
        }

        Debug.Log($"({id}) is not available dialog");
        return false;
    }



    public void Init() {
        if (IsInit) return;



        _isInit = true;
    }
    public void Release() {
        throw new System.NotImplementedException();
    }
}
