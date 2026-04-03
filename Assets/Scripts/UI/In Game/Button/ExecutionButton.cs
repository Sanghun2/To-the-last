using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class ExecutionButton : ButtonBase
{
    [SerializeField] TextMeshProUGUI buttonText;
    private ActionData actionData;

    public void SetExecuteAction(ActionData actionData) {
        this.actionData = actionData;
    }

    protected override void ButtonAction() {
        actionData?.Action?.Invoke();
    }


    private void OnDisable() {
        actionData = null;
    }
}
