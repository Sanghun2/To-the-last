using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class LootSelectionRunnerContext : SelectionRunnerContextBase, IInventoryContext
{
    public IReadOnlyList<InventoryBase> TargetInventories { get; }

    public LootSelectionRunnerContext(LootSelectionRunnerData data) : base(data.RequireMinutes) {
        TargetInventories = Managers.Inventory.TryGetInventoryByTag(Define.Tag.PLAYER, out var inventory) ? inventory : null;
    }
}
