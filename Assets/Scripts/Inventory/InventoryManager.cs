using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

// ingame에서 사용되는 player, storage 등의 인벤토리 정보를 관리하는 Manager
public sealed class InventoryManager
{
    private Dictionary<string, InventoryBase> inventoryDict = new();
    private Dictionary<string, List<InventoryBase>> inventoryCategories = new();

    public void AddInventory(InventoryBase inventory) {
        if (!inventoryDict.TryAdd(inventory.InventoryID, inventory)) { Debug.LogError($"<color=red>이미 있는 인벤토리 id? {inventory.InventoryID}</color>"); return; }

        if (inventoryCategories.TryGetValue(inventory.Tag, out var inventories)) {
            inventories.Add(inventory);
        }
        else {
            inventoryCategories[inventory.Tag] = new List<InventoryBase>() { inventory };
        }
    }

    public void RemoveInventory(InventoryBase inventory) {
        RemoveInventory(inventory.InventoryID, inventory.Tag);
    }
    public void RemoveInventory(string inventoryID, string tag) {
        inventoryDict.Remove(inventoryID);

        if (inventoryCategories.TryGetValue(tag, out var inventories)) {
            var index = inventories.FindIndex(x => x.InventoryID.Equals(inventoryID));
            if (index >= 0) {
                inventories.RemoveAt(index);
            }
        }
    }

    public bool TryGetInventoryByID(string inventoryID, out InventoryBase inventory) {
        return inventoryDict.TryGetValue(inventoryID, out inventory);
    }
    public bool TryGetInventoryByTag(string tag, out List<InventoryBase> inventoryList) {
        if (inventoryCategories.TryGetValue(tag, out inventoryList)) {
            return true;
        }

        return false;
    }
    public bool TryGetInventoryByTag(out List<InventoryBase> inventoryList, params string[] tags) {
        inventoryList = new List<InventoryBase>();
        for (int i = 0; i < tags.Length; i++) {
            if (!inventoryCategories.TryGetValue(tags[i], out var list)) {
                Debug.LogWarning($"tag ({tags[i]})에 해당하는 inventory 없음");
                continue;
            }
            inventoryList.AddRange(list);
        }

        return inventoryList.Count > 0;
    }
}
