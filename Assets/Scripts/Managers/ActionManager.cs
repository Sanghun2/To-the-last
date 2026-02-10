using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : IInitializable
{
    public bool IsInit => throw new NotImplementedException();

    private Dictionary<Type, ActionBase> actionDict = new Dictionary<Type, ActionBase>();
    private bool _isInit;

    public void Init() {
        if (IsInit) return;


        _isInit = true;
    }

    public void RegisterAction<TAction>(ActionBase actionBase) where TAction : ActionBase {
        actionDict[typeof(TAction)] = actionBase;
    }

    public void Release() {
        actionDict.Clear();
    }
}

public abstract class ActionBase
{
    public abstract void Execute();
}

public abstract class ActionBase<T> : ActionBase
{
    protected T parameter;

    public void SetParameter(T parameter) {
        this.parameter = parameter;
    }
}