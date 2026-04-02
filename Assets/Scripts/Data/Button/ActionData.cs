using System;
using UnityEngine;

public class ActionData
{
    public string Text => text;
    public Action Action => action;
    public Func<bool> CanExecute => canExecute;

    protected string text;
    protected Action action;
    protected Func<bool> canExecute;

    public ActionData(string text, Action action, Func<bool> canExecute=null) {
        this.text = text;
        this.action = action;
        this.canExecute = canExecute ?? (() => true);
    }

    public ActionData(Action action) {
        this.text = null;
        this.action = action;
        this.canExecute = canExecute ?? (() => true);
    }
}
