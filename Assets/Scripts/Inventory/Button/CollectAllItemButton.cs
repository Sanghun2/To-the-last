using System;
using BilliotGames;
using UnityEngine;

public class CollectAllItemButton : ButtonBase
{
    [SerializeField] InventoryUIBase fromInventoryUI;
    [SerializeField] InventoryUIBase toInventoryUI;

    protected override void ButtonAction() {
        if (fromInventoryUI == null || toInventoryUI == null) { Debug.LogError($"<color=red>inventory is null</color>"); return; }

        CollectItems(fromInventoryUI.Inventory, toInventoryUI.Inventory);
    }

    private void CollectItems(InventoryBase fromInventory, InventoryBase toInventory) {
        InventoryUtility.MoveItems(fromInventory, toInventory);
    }
}