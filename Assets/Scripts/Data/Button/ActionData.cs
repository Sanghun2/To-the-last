using System;
using UnityEngine;

public class ActionData
{
    public string Text => text;
    public Action Action => action;

    private string text;
    private Action action;

    public ActionData(string text, Action action) {
        this.text = text;
        this.action = action;
    }

    public ActionData(Action action) {
        this.text = null;
        this.action = action;
    }
}
