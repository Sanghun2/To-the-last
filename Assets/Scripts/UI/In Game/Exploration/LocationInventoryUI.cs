using BilliotGames;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class LocationInventoryUI : UIBase
{
    [SerializeField] ItemStorageInventoryUI topInventoryUI;
    [SerializeField] ItemStorageInventoryUI bottomInventoryUI;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public void ShowInventory(string locationID) {
        if (!Managers.Inventory.TryGetInventoryByID(locationID, out InventoryBase locationInventory)) {
            locationInventory = Managers.Inventory.AddInventory(new SimpleInventory(locationID, 50));
        }

        var playerInven = Managers.Player.PlayerData.Inventory;

        ShowInventory(locationInventory, playerInven);
    }

    public void ShowInventory(InventoryBase top, InventoryBase bottom) {
        InitUI();

        topInventoryUI.ShowInventory(top);
        bottomInventoryUI.ShowInventory(bottom);
        OpenUI();
    }
}
