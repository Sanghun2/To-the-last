using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public interface IInventoryContext : IContext
{
    IReadOnlyList<InventoryBase> TargetInventories { get; }
}
