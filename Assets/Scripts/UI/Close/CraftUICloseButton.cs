using BilliotGames;
using UnityEngine;

public class CraftUICloseButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.CloseUI<CraftUI>();
    }
}
