using BilliotGames;
using UnityEngine;

public class ExitButton : ButtonBase
{
    protected override void ButtonAction() {
        var explorationUI = Managers.UI.GetUI<ExplorationUI>();
        explorationUI.ShowEnterance();
    }
}
