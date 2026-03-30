using System.Collections.Generic;
using System.Linq;
using System.Text;
using BilliotGames;
using UnityEngine;

public class SimpleItemMoveProcessor : ItemMoveProcessorBase
{
    public override void MoveAllItems(InventoryBase fromInventory, InventoryBase toInventory) {
        if (!(fromInventory is SimpleInventory invenFrom && toInventory is SimpleInventory invenTo)){
            Debug.LogError($"<color=red>different inven type. target? {fromInventory.GetType()}, player? {toInventory.GetType()}</color>"); 
            return;
        }

        var targetItemList = new List<ItemStack>(invenFrom.ItemList); // 복사본으로 순회

        // 무거운 순으로 정렬
        targetItemList.Sort((x,y) => {
            var xData = x.ItemData as ExtendedItemData;
            var yData = y.ItemData as ExtendedItemData;
            return -(xData.Weight.CompareTo(yData.Weight));
        });

//#if UNITY_EDITOR
//        StringBuilder sb = new StringBuilder();
//        sb.AppendLine("sorted list");
//        for (int i = 0; i < targetItemList.Count; i++) {
//            var item = targetItemList[i];
//            sb.AppendLine($"{item.ItemData.ItemID}, {item.Amount}");
//        }
//        Debug.Log(sb.ToString());
//#endif

        for (int i = 0; i < targetItemList.Count; i++) {
            var itemData = targetItemList[i];
            var copiedItem = new ItemStack(itemData.ItemData, itemData.Amount); // 복사본 전달
            if (invenTo.TryPushItem(copiedItem, out ItemStack overflowedItem)) {
                int movedAmount = itemData.Amount - (overflowedItem?.Amount ?? 0);
                invenFrom.TryRemoveItem(itemData.ItemData.ItemID, movedAmount);
            }
        }
    }
}
