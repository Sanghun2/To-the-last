using BilliotGames;
using UnityEngine;

public class ShopUI : UIBase
{
    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }
}
