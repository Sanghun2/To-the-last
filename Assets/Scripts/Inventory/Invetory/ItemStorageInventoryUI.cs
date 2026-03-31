using BilliotGames;
using UnityEngine;

public class ItemStorageInventoryUI : InventoryUIBase<SimpleInventory>
{
    [SerializeField] ItemSlotContainer itemSlotContainer;
    [SerializeField] WeightUI weightUI;

    private void OnEnable() {
        if (inventory != null) {
            inventory.OnItemAdded -= OnAddItem;
            inventory.OnItemAdded += OnAddItem;

            bool hasWeightCounter = inventory.InventoryID.Equals(Define.Tag.PLAYER) || inventory.InventoryID.Equals(Define.Tag.STORAGE);

            weightUI.gameObject.SetActive(hasWeightCounter);
            if (hasWeightCounter) {
                var simpleInventory = (SimpleInventory)inventory;
                weightUI.SetWeightCounter(simpleInventory.WeightCounter);
            }
        }
    }

    public override InventoryUIBase InitInventory(InventoryBase inventory) {
        if (IsInit) return this;

        itemSlotContainer.InitUI();
        base.InitInventory(inventory);

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
