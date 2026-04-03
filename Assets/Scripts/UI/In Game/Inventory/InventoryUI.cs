using BilliotGames;
using UnityEngine;

public class InventoryUI : InventoryUIBase
{
    [SerializeField] ItemUIContainer itemUIContainer;

    public override void InitUI() {
        CloseUI();
    }

    public override void ShowInventory(InventoryBase inventoryBase) {
        InitUI();
        if (inventoryBase is SimpleInventory inventory) {
            var itemList = inventory.ItemList;
            for (int i = 0; i < itemList.Count; ++i) {
                ItemStack item = itemList[i];

                SimpleItemUI itemUI = itemUIContainer.GetOrCreateObj(i);
                itemUI.SetUI(item.ToArgs());
                item.OnAmountChanged -= itemUI.UpdateUI;
                item.OnAmountChanged += itemUI.UpdateUI;
            }
        }
        else {
            Debug.LogError($"inventory type not matched. require type? {typeof(SimpleInventory)}, current type? {inventoryBase.GetType()}");
        }

        OpenUI();
    }

    private void Reset() {
        if (itemUIContainer == null) {
            itemUIContainer = GetComponentInChildren<ItemUIContainer>();
        }
    }
}
