using System;
using BilliotGames;
using UnityEngine;

public class ExecutionButton : ButtonBase
{
    private Action action;

    public void SetAction(Action action) {
        this.action = action;
    }

    protected override void ButtonAction() {
        action?.Invoke();
    }

    private void OnDisable() {
        action = null;
    }
}
