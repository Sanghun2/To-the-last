using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class GameBootStrapUI : UIBase
{
    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }
}
