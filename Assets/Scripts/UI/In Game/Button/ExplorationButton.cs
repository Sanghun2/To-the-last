using BilliotGames;
using UnityEngine;

public class ExplorationButton : ButtonBase
{
    protected override void ButtonAction() {
        var mapUI = Managers.UI.GetUI<MapUI>();
        mapUI.InitUI();
        mapUI.OpenUI();
        Managers.UI.CloseUI<BasementUI>();
    }
}
