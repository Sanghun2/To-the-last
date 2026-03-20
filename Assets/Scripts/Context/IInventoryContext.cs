using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public interface IInventoryContext
{
    IReadOnlyList<InventoryBase> Inventories { get; }
}
