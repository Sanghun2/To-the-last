using BilliotGames;
using UnityEngine;

public class BackButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.CloseTopUI();
    }
}
