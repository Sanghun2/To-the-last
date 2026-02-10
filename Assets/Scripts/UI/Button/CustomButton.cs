using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : ButtonBase, IContent
{
    public bool IsActive => IsOpened;

    [SerializeField] TextMeshProUGUI buttonText;

    public void Init() {
        InitUI();
    }
    public void Activate() {
        OpenUI();
    }
    public void Release() {
        CloseUI();
    }

    public void InitButton(ActionData actionData) {
        Init();
        buttonText.text = actionData.Text;
        SetButtonAction(actionData.Action);
    }

    protected override void ButtonAction() {
        // button base의 set button action으로 할당해서 사용
    }
}
