using BilliotGames;
using UnityEngine;

public class SettingButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.UI.OpenUI<SettingUI>();
    }
}
