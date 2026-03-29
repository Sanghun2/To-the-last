using System;
using BilliotGames;
using UnityEngine;

public class LocationInventoryUI : InventoryUIBase<SimpleInventory>
{
    [SerializeField] ItemSlotContainer itemSlotContainer;

    private void OnEnable() {
        if (inventory != null) {
            inventory.OnItemAdded -= OnAddItem;
            inventory.OnItemAdded += OnAddItem;
        }
    }

    public override InventoryUIBase InitInventory(InventoryBase inventory) {
        if (IsInit) return this;

        itemSlotContainer.InitUI();
        base.InitInventory(inventory);
        inventory.OnItemAdded -= OnAddItem;
        inventory.OnItemAdded += OnAddItem;

        _isInit = true;
        return this;
    }

    public override void ShowInventory(SimpleInventory simpleInventory) {
        var list = simpleInventory.ItemList;

        itemSlotContainer.Clear();
        for (int i = 0; i < list.Count; i++) {
            ItemStack itemStack = list[i];
            ItemSlotUIBase itemSlot = itemSlotContainer.GetOrCreateObj(i);
            itemSlot.SetSlotUI(itemStack);
        }
    }


    private void OnDisable() {
        if (inventory != null) {
            inventory.OnItemAdded -= OnAddItem;
        }
    }
    private void OnAddItem(ItemStack stack, int delta) {
        var itemSlot = itemSlotContainer.GetObj();
        itemSlot.SetSlotUI(stack);
    }
}
