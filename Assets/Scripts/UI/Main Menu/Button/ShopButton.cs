using BilliotGames;
using UnityEngine;

public class ShopButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.OpenUI<ShopUI>();
    }
}
