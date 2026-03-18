using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusHandler
{
    private Dictionary<string, Status> statusDict = new Dictionary<string, Status>();

    public event Action<Status> OnStatusAdded;
    public event Action<Status, int> OnStackAdded;
    public event Action<Status, int> OnStatusRemoved;

    public void AddStatus(Status inputStatus) {
        if (inputStatus == null) { Debug.LogError($"<color=red>status is null </color>"); return; }
        if (statusDict.TryGetValue(inputStatus.ID, out var baseStatus)) {
            if (!baseStatus.TryAdd(inputStatus)) return;
            OnStackAdded?.Invoke(baseStatus, inputStatus.CurrentStack);
        }
        else {
            statusDict[inputStatus.ID] = inputStatus;
            OnStatusAdded?.Invoke(inputStatus);
        }
    }

    public void RemoveStatus(string statusID) {
        if (!statusDict.TryGetValue(statusID, out var status)) return;

        statusDict.Remove(statusID);
        OnStatusRemoved(status, status.CurrentStack);
    }

    public void RemoveStatus(string statusID, int stack, bool allowOver=true) {
        if (!statusDict.TryGetValue(statusID, out var status)) return;

        int baseStack = status.CurrentStack;
        if (status.TryRemove(stack, allowOver)) {
            OnStatusRemoved?.Invoke(status, baseStack - status.CurrentStack);
        }
    }
}
