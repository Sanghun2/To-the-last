using BilliotGames;
using UnityEngine;

public class BasementUI : UIBase
{
    public override void InitUI() {
        if (IsInit) return;

        OpenUI();

        _isInit = true;
    }
}
