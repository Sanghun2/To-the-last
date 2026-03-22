using BilliotGames;
using UnityEngine;

public class RankingButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.OpenUI<RankingUI>();
    }
}
