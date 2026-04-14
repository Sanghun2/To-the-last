using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class TradeScreen : UIBase
{
    [SerializeField] ItemStorageInventoryUI storageInventoryUI;

    public void InitUI(InventoryBase npcInventory) {
        storageInventoryUI.ShowInventory(npcInventory);
    }
}
