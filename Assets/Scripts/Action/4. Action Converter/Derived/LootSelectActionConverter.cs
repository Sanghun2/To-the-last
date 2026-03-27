using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BilliotGames;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSelectActionConverter : SelectActionConverterBase<LootSelectActionContext>
{
    protected override Action ExecuteAction(LootSelectActionContext lootContext) {
        return () => {
            Debug.Log($"({lootContext.GetType()}) action executed");
            LootItems(lootContext);
        };
    }

    private void LootItems(LootSelectActionContext lootContext) {
        var targetInven = lootContext.TargetInventories[0];
        LootSelectionData lootSD = (LootSelectionData)lootContext.SelectionData;

        if (lootContext.SelectionData is LootSelectionData lootData) {
            // 기본 아이템
            var itemDict = GetDefaultItems(lootData);

            // 랜덤 획득
            itemDict = GetAdditionalItems(itemDict, lootData, lootContext);



#if UNITY_EDITOR
            var sb = new StringBuilder().AppendLine("Looted Item List");
            foreach (var item in itemDict) {
                sb.AppendLine($"{item.Key} - {item.Value}");
            }
            Debug.Log(sb.ToString());

#endif
            foreach (var item in itemDict) {
                string itemID = item.Key;
                int amount = item.Value;

                var targetInventory = lootContext.TargetInventories[0];
                if (!targetInventory.TryPushItem(new ItemStack(new ItemData(itemID, 999), amount), out var overflowedStack)) {
                    Debug.Log($"push item inventory({targetInventory.InventoryID}) failed");
                }
            }
        }
    }

    private Dictionary<string, int> GetDefaultItems(LootSelectionData lootSelectionData) {
        var lootItemDataList = lootSelectionData.AvailableItemList;
        Dictionary<string, int> itemDict = new(lootItemDataList.Count);
        for (int i = 0; i < lootItemDataList.Count; i++) {
            LootInfo lootItemData = lootItemDataList[i];
            string itemID = lootItemData.ItemSD.ID;
            itemDict.TryAdd(itemID, lootItemData.MinAppearence);
        }

        return itemDict;
    }
    private Dictionary<string, int> GetAdditionalItems(
        Dictionary<string, int> itemDict,
        LootSelectionData lootSelectionData,
        LootSelectActionContext context) {

        var lootItemDataList = lootSelectionData.AvailableItemList;
        var weightSum = lootItemDataList.Sum(data => data.Weight);
        int lootCount = (int)(lootSelectionData.DefaultLootCount * context.LootCountMutiflier);
        Debug.Log($"loot count: {lootCount}, weight: {weightSum}");

        for (int l = 0; l < lootCount; l++) {
            int targetWeight = Random.Range(1, weightSum + 1);
            int currentWeight = 0;
            for (int j = 0; j < lootItemDataList.Count; j++) {
                LootInfo lootItem = lootItemDataList[j];
                currentWeight += lootItem.Weight;
                if (currentWeight < targetWeight) continue;

                string itemID = lootItem.ItemSD.ID;
                itemDict.TryGetValue(itemID, out int count);
                itemDict[itemID] = count + 1;
                break;
            }
        }

        return itemDict;
    }
}