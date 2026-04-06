using System;
using BilliotGames;
using UnityEngine;

public class CollectAllItemButton : ButtonBase
{
    [SerializeField] InventoryUIBase fromInventoryUI;
    [SerializeField] InventoryUIBase toInventoryUI;

    protected override void ButtonAction() {
        if (fromInventoryUI == null || toInventoryUI == null) { Debug.LogError($"<color=red>inventory is null</color>"); return; }

        var fromInven = Managers.Location.CurrentLocation.Inventory;
        var toInven = Managers.Player.PlayerData.Inventory;

        Managers.Sound.PlaySound(Define.Sound.COLLECT);
        CollectItems(fromInven, toInven);
    }

    private void CollectItems(InventoryBase fromInventory, InventoryBase toInventory) {
        InventoryUtility.MoveItems(fromInventory, toInventory);
        fromInventoryUI.ShowInventory(fromInventory);
        toInventoryUI.ShowInventory(toInventory);
    }
}