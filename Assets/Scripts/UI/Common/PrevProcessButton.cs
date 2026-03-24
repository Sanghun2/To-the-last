using BilliotGames;
using UnityEngine;

public class PrevProcessButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.Process.ExecutePrevProcess();
    }
}
