using BilliotGames;
using UnityEngine;

public class InventoryUI : InventoryUIBase
{
    [SerializeField] ItemUIContainer itemUIContainer;

    public override void ShowInventory(InventoryBase inventoryBase) {
        if (inventoryBase is SimpleInventory inventory) {
            var itemList = inventory.ItemList;
            for (int i = 0; i < itemList.Count; ++i) {
                ItemStack item = itemList[i];
                if (!itemUIContainer.TryGetObj(i, out SimpleItemUI itemUI)) {
                    itemUI = itemUIContainer.CreateObj();
                }

                itemUI.InitItem(item.ItemData, item.Amount);
                item.OnAmountChanged -= itemUI.UpdateUI;
                item.OnAmountChanged += itemUI.UpdateUI;
            }
        }
        else {
            Debug.LogError($"inventory type not matched. require type? {typeof(SimpleInventory)}, current type? {inventoryBase.GetType()}");
        }
    }

    private void Reset() {
        if (itemUIContainer == null) {
            itemUIContainer = GetComponentInChildren<ItemUIContainer>();
        }
    }
}
