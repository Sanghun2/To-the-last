using System;
using BilliotGames;
using UnityEngine;

public abstract class InventoryUIBase : UIBase
{
    public InventoryBase Inventory => inventory;

    protected InventoryBase inventory;

    public virtual InventoryUIBase InitInventory(InventoryBase inventory) {
        SetInventory(inventory);
        return this;
    }
    public void SetInventory(InventoryBase targetInventory) {
        this.inventory = targetInventory;
    }

    public abstract void ShowInventory(InventoryBase inventoryBase);
    public void ShowInventory() {
        ShowInventory(inventory);
    }
}

public abstract class InventoryUIBase<TInventory> : InventoryUIBase
    where TInventory : InventoryBase
{
    public override void ShowInventory(InventoryBase inventoryBase) {
        if (inventoryBase is TInventory inventory) {
            ShowInventory(inventory);
        }
    }

    public abstract void ShowInventory(TInventory inventoryBase);
}