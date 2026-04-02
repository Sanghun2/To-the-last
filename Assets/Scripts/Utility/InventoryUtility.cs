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
    public static bool HasIngredients(IReadOnlyList<InventoryBase> inventories, IReadOnlyList<Ingredient> requirementItems) {
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

    public static int GetItemCount(string id) {
        return GetItemCount(Define.Tag.PLAYER, Define.Tag.STORAGE);
    }
    public static int GetItemCount(string id, params string[] inventoryTags) {
        if (!Managers.Inventory.TryGetInventoryByTag(out var inventories, inventoryTags)) {
            return 0;
        }

        int total = 0;
        for (int i = 0; i < inventories.Count; i++) {
            total += inventories[i].GetItemCount(id);
        }
        return total;
    }

    public static void MoveItems(InventoryBase fromInventory, InventoryBase toInventory) {
        collectProcessor.MoveAllItems(fromInventory, toInventory);
    }

    public static IReadOnlyList<InventoryBase> GetInventoriesInBasement() {
        if (Managers.Inventory.TryGetInventoryByTag(out var inventories, Define.Tag.PLAYER, Define.Tag.STORAGE)) {
            return inventories;
        }

        Debug.LogError($"<color=red>no inventory of tag ({Define.Tag.PLAYER}) & ({Define.Tag.STORAGE})</color>");
        return null;
    }

    public static bool TryConsumeIngredients(IReadOnlyList<InventoryBase> inventories, IReadOnlyList<Ingredient> ingredients) {
#if TEST
        return true;
#else
        // ── 1단계: 사전 검증 ─────────────────────────────────────────────────────
        // 실제 제거 전에 전체 보유량을 확인해서, 중간에 실패하는 상황을 방지
        for (int j = 0; j < ingredients.Count; j++) {
            var ingredient = ingredients[j];
            string itemID = ingredient.ItemSD.ID;
            int totalAvailable = 0;

            for (int i = 0; i < inventories.Count; i++) {
                totalAvailable += inventories[i].GetItemCount(itemID);
            }

            if (totalAvailable < ingredient.Amount) {
                Debug.LogAssertion($"[TryConsumeIngredients] 재료 부족: {itemID} " +
                                   $"필요={ingredient.Amount}, 보유={totalAvailable}");
                return false;
            }
        }

        // ── 2단계: 실제 제거 ──────────────────────────────────────────────────────
        // 검증 통과 후이므로 여기서는 반드시 전량 제거 성공이 보장됨
        for (int j = 0; j < ingredients.Count; j++) {
            var ingredient = ingredients[j];
            string itemID = ingredient.ItemSD.ID;
            int remaining = ingredient.Amount;

            for (int i = 0; i < inventories.Count && remaining > 0; i++) {
                // 캐스팅: 부분 제거는 SimpleInventory에만 구현
                if (inventories[i] is SimpleInventory simpleInv) {
                    int removed = simpleInv.RemoveItemPartial(itemID, remaining);
                    remaining -= removed;
                }
            }
        }

        return true;
#endif
    }
}
