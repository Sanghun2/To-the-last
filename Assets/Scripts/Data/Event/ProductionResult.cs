using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public readonly struct ProductionResult
{
    public bool IsEmpty => producedItems == null || producedItems.Count == 0;

    public readonly IReadOnlyList<ProducedItemInfo> producedItems;
    public readonly InGameTimeArgs completedAt;

    public ProductionResult(IReadOnlyList<ProducedItemInfo> producedItems, InGameTimeArgs completedAt) {
        this.producedItems = producedItems;
        this.completedAt = completedAt;
    }
}
public readonly struct ProducedItemInfo
{
    public readonly string itemID;
    public readonly int amount;

    public ProducedItemInfo(string itemID, int amount) {
        this.itemID = itemID;
        this.amount = amount;
    }
}