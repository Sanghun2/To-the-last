using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class GamePrepareUI : UIBase
{
    private List<ProcessChain> processChains = new List<ProcessChain>();

    public override void InitUI() {
        if (IsInit) return;



        _isInit = true;
    }

    public void PrepareGame() {
        // 연출

        // 체인 시작

    }
}
