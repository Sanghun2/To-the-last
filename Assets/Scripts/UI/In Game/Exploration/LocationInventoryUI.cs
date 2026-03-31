using BilliotGames;
using UnityEngine;

public class LocationInventoryUI : UIBase
{
    [SerializeField] ItemStorageInventoryUI locationInventoryUI;
    [SerializeField] ItemStorageInventoryUI playerInventoryUI;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        playerInventoryUI.InitInventory(Managers.Player.PlayerData.Inventory);

        _isInit = true;
    }

    public void ShowInventory(string locationID) {
        locationInventoryUI.SetInventory(Managers.Inventory.TryGetInventoryByID(locationID, out InventoryBase locationInventory) ? locationInventory : null);
        locationInventoryUI.ShowInventory();
        playerInventoryUI.ShowInventory();
    }
}
