using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BilliotGames;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSelectActionConverter : SelectActionConverterBase<LootSelectActionContext>
{
    protected override Action SelectAction(LootSelectActionContext lootContext) {
        return () => {
            Debug.Log($"({lootContext.GetType()}) action executed");
            var lootedItems = LootItems(lootContext);


            foreach (var item in lootedItems) {
                string itemID = item.Key;
                int amount = item.Value;
                if (!Managers.SD.TryGetSD(itemID, out ItemSD itemSD)) { Debug.LogError($"({itemID}) sd data is not exist"); continue; }

                if (!InventoryUtility.TryPushItem(
                    lootContext.TargetInventories, 
                    new ItemStack(new ExtendedItemData(itemID, 999, itemSD.Weight), amount))) {
                    //Debug.Log($"push item inventory({targetInventory.InventoryID}) failed");
                }
            }

            Managers.UI.OpenUI<LocationInventoryUI>().ShowInventory(lootContext.LocationID);
        };
    }

    private IReadOnlyDictionary<string, int> LootItems(LootSelectActionContext lootContext) {
        var targetInven = lootContext.TargetInventories[0];
        if (lootContext.SelectionData is LootSelectionData lootData) {
            // 기본 아이템
            var lootedItemDict = GetDefaultLootItems(lootData);

            // 랜덤 획득
            lootedItemDict = GetAdditionalLootItems(lootedItemDict, lootData, lootContext);



#if UNITY_EDITOR
            var sb = new StringBuilder().AppendLine("Looted Item List");
            foreach (var item in lootedItemDict) {
                sb.AppendLine($"{item.Key} - {item.Value}");
            }
            Debug.Log(sb.ToString());

#endif

            return lootedItemDict;
        }
        else {
            Debug.LogError($"<color=red>item dict is not type of ({typeof(LootSelectionData)})</color>");
            return null;
        }
    }

    private Dictionary<string, int> GetDefaultLootItems(LootSelectionData lootSelectionData) {
        var lootItemDataList = lootSelectionData.AvailableItemList;
        Dictionary<string, int> itemDict = new(lootItemDataList.Count);
        for (int i = 0; i < lootItemDataList.Count; i++) {
            LootInfo lootItemData = lootItemDataList[i];
            string itemID = lootItemData.ItemSD.ID;
            itemDict.TryAdd(itemID, lootItemData.MinAppearence);
        }

        return itemDict;
    }
    private Dictionary<string, int> GetAdditionalLootItems(
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