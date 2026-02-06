using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class PlacementLocationUI : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.OpenUI<BuildingUI>();
    }
}
