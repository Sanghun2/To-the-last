using BilliotGames;
using UnityEngine;

public class ExplorationButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.OpenUI<MapUI>();
        Managers.UI.CloseUI<BasementUI>();
    }
}
