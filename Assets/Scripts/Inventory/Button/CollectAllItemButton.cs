using System;
using BilliotGames;
using UnityEngine;

public class CollectAllItemButton : ButtonBase
{
    [SerializeField] InventoryUIBase targetInventory;
    [SerializeField] InventoryUIBase playerInventory;
    private ItemCollectProcessorBase collectProcessor = new SimpleItemCollectProcessor();

    protected override void ButtonAction() {
        if (targetInventory == null || playerInventory == null) { Debug.LogError($"<color=red>inventory is null</color>"); return; }

        collectProcessor.CollectAllItems(targetInventory.Inventory, playerInventory.Inventory);
    }
}