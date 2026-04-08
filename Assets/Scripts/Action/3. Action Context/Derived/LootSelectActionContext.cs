using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class LootSelectActionContext : SelectActionContextBase, IInventoryContext
{
    public IReadOnlyList<InventoryBase> TargetInventories => inventories;
    public string LocationID { get; }
    public int LootCountMutiflier => 1;



    private List<InventoryBase> inventories = new List<InventoryBase>();

    public LootSelectActionContext(SelectionRunnerDataBase selectionRunnerData, InventoryBase inventory, string locationID) 
        : base(selectionRunnerData, selectionRunnerData.RequireMinutes) {
        inventories.Add(inventory);
        LocationID = locationID;
    }
}