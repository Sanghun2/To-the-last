using System;
using BilliotGames;
using UnityEngine;

public class ItemManager
{
    public bool TryPushItem(InventoryBase inventory, ItemStack inputStack, out ItemStack overflowedStack) {
        if (inventory.TryPushItem(inputStack, out overflowedStack)) {
            return true;
        }

        return false;
    }

    public bool TryRemoveItem(InventoryBase inventory, string itemID, int amount) {
        if (inventory.TryRemoveItem(itemID, amount)) {
            return false;
        }

        return true;
    }
}
