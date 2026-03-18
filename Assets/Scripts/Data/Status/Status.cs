using System;
using UnityEngine;

public class Status
{
    public string ID => statusData.ID;
    public int CurrentStack => currentStack;


    [SerializeField] StatusData statusData;
    [SerializeField] int currentStack;

    public Status(StatusData data) {
        statusData = data;
    }

    public bool TryAdd(Status status) {
        if (CanStack(status)) {
            currentStack += status.currentStack;
            return true;
        }

        return false;
    }
    public bool TryRemove(int stack, bool allowOver) {
        if (allowOver) {
            currentStack = Mathf.Max(currentStack-stack, 0);
            return true;
        }

        if (currentStack >= stack) {
            currentStack -= stack;
            return true;
        }

        return false;
    }


    private bool CanStack(Status inputStatus) {
        int resultStack = currentStack + inputStatus.currentStack;
        int maxStack = statusData.MaxStack;
        if (maxStack == -1 || resultStack <= maxStack) {
            return true;
        }

        return false;
    }
}
