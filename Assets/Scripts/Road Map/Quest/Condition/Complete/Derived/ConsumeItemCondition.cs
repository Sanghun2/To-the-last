using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class ConsumeItemCondition : ITaskCompleteCondition, IInventoryContext
{
    public IReadOnlyList<InventoryBase> TargetInventories { get; }
    private string itemID;
    private int amount;

    public ConsumeItemCondition(string itemID, int amount, IReadOnlyList<InventoryBase> targetInventories) {
        TargetInventories = targetInventories;
        this.itemID = itemID;
        this.amount = amount;
    }

    public bool Execute() {
        return TargetInventories.TryRemoveItem(itemID, amount);
    }
}
