using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class InventoryUtility
{
    private readonly static ItemMoveProcessorBase collectProcessor = new SimpleItemMoveProcessor();

    public static bool HasIngredients(IReadOnlyList<Ingredient> requirementItems) {
        if (Managers.Inventory.TryGetInventoryByTag(out var targetInventories, Define.Tag.PLAYER, Define.Tag.STORAGE)) {
            return HasIngredients(targetInventories, requirementItems);
        }

        return false;
    }
    public static bool HasIngredients(List<InventoryBase> inventories, IReadOnlyList<Ingredient> requirementItems) {
        bool result = true;
        for (int i = 0; i < requirementItems.Count; i++) {
            Ingredient item = requirementItems[i];
            int totalCount = 0;
            for (int j = 0; j < inventories.Count; j++) {
                var inventory = inventories[j];
                totalCount += inventory.GetItemCount(item.ItemSD.ID);

                if (totalCount >= item.Amount) break;
            }

            if (totalCount < item.Amount) {
                return false;
            }
        }

        return result;
    }

    public static void MoveItems(InventoryBase fromInventory, InventoryBase toInventory) {
        collectProcessor.MoveAllItems(fromInventory, toInventory);
    }

    internal static bool HasIngredients(List<InventoryBase> inventories, object requirementItems) {
        throw new NotImplementedException();
    }
}
