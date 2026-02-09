using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : ButtonBase, IContent
{
    public bool IsActive => IsOpened;

    public void Init() {

    }
    public void Activate() {
        OpenUI();
    }
    public void Release() {
        CloseUI();
    }

    protected override void ButtonAction() {
        // button base의 set button action으로 할당해서 사용
    }
}
