using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class GameBootStrapUI : UIBase
{
    private List<ProcessChain> processChains = new List<ProcessChain>();

    public override void InitUI() {
        if (IsInit) return;



        _isInit = true;
    }
}
