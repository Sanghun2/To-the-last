using System;
using BilliotGames;
using UnityEngine;

public class TradeUI : LocationUIBase<TradeNPCLocation>
{
    [SerializeField] InventoryViewUI tradeView;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public override void InitLocationUI(TradeNPCLocation location) {
        InitUI();
        tradeView.CloseUI();
        EnteranceUI.InitEnteracne(location, new ActionData("거래", () => {
            HideEnterance();
            tradeView.ShowInventory(
                Managers.Player.PlayerData.Inventory, 
                location.Inventory,
                null);
            tradeView.OpenUI();
        }));
    }

    protected override void OnShowEnterance() {
        tradeView.CloseUI();
    }
}
