using BilliotGames;
using UnityEngine;

public class SettingUI : UIBase
{
    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }
}
