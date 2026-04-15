using System;
using BilliotGames;
using UnityEngine;

public class NextProcessButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.Process.TryCompleteCurrentProcess();
    }
}
