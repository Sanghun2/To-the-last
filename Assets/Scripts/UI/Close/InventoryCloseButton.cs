using BilliotGames;
using UnityEngine;

public class InventoryCloseButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.CloseUI<InventoryUI>();
    }
}
