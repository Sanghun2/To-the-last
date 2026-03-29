using System.Collections.Generic;
using BilliotGames;

// ingame에서 사용되는 player, storage 등의 인벤토리 정보를 관리하는 Manager
public sealed class InventoryManager
{
    private Dictionary<string, InventoryBase> inventoryDict = new();

    public void AddInventory(InventoryBase inventory) {
        inventoryDict[inventory.InventoryID] = inventory;
    }
    public void ReomveInventory(string inventoryID) {
        inventoryDict.Remove(inventoryID);
    }

    public bool TryGetInventory(string inventoryID, out InventoryBase inventory) {
        return inventoryDict.TryGetValue(inventoryID, out inventory);
    }
}
