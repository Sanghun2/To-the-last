using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class LootSelectActionContext : SelectActionContextBase, IInventoryContext
{
    public IReadOnlyList<InventoryBase> TargetInventories => inventories;

    public int LootCountMutiflier => 1;

    private List<InventoryBase> inventories = new List<InventoryBase>();

    public LootSelectActionContext(SelectionDataBase selectionData, InventoryBase inventory) : base(selectionData, selectionData.RequireMinutes) {
        inventories.Add(inventory);
    }
}