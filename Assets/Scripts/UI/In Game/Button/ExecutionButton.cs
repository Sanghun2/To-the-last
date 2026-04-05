using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionButton : ButtonBase
{
    [SerializeField] TextMeshProUGUI buttonText;
    private ActionData actionData;

    public void SetExecuteAction(string text) {
        buttonText.text = text;
    }
    public void SetExecuteAction(ActionData actionData) {
        buttonText.text = actionData.Text;
        this.actionData = actionData;
    }
    public void SetInteractable(bool interactable) {
        targetButton.interactable = interactable;
    }

    protected override void ButtonAction() {
        actionData?.Action?.Invoke();
    }


    private void OnDisable() {
        actionData = null;
    }
}
