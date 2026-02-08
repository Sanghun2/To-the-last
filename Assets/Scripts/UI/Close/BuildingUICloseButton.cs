using BilliotGames;
using UnityEngine;

public class BuildingUICloseButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.CloseUI<BuildingUI>();
    }
}
