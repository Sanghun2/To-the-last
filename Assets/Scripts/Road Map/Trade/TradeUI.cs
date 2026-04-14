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
            Managers.UI.OpenUI(tradeView);
        }));
    }

    protected override void OnShowEnterance() {
        tradeView.CloseUI();
    }

    private void OnEnable() {
        tradeView.OnClosed -= ShowEnterance;
        tradeView.OnClosed += ShowEnterance;
    }
    private void OnDisable() {
        tradeView.OnClosed -= ShowEnterance;
    }
}
