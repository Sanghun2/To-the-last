using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BilliotGames;
using UnityEngine;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class LootSelectionContext : SelectionContext
{
    public float LootCountMutiflier => lootCountMultiflier;

    [SerializeField] float lootCountMultiflier = 1;

    public LootSelectionContext SetLootCountMultiflier(float value) {
        lootCountMultiflier = value;
        return this;
    }
}

public class LootSelectionHandler : SelectionHandler<LootSelectionSD, LootSelectionContext>
{
    public override void Execute(LootSelectionSD selectionSD, LootSelectionContext context = null) {
        // 기본 아이템
        var itemDict = GetDefaultItems(selectionSD);

        // 랜덤 획득
        itemDict = GetAdditionalItems(itemDict, selectionSD, context);


        // 추가 보상 조정

        var sb = new StringBuilder().AppendLine("Looted Item List");
        foreach (var item in itemDict) {
            sb.AppendLine($"{item.Key} - {item.Value}");
        }
        Debug.Log(sb.ToString());
    }

    private Dictionary<string, int> GetAdditionalItems(
        Dictionary<string, int> itemDict, 
        LootSelectionSD lootSelectionSD, 
        LootSelectionContext context) {

        var lootItemDataList = lootSelectionSD.LootItemDataList;
        var weightSum = lootItemDataList.Sum(data => data.Weight);
        int lootCount = (int)(lootSelectionSD.DefaultLootCount * context.LootCountMutiflier);
        Debug.Log($"loot count: {lootCount}, weight: {weightSum}");

        for (int l = 0; l < lootCount; l++) {
            int targetWeight = Random.Range(1, weightSum+1);
            int currentWeight = 0;
            for (int j = 0; j < lootItemDataList.Count; j++) {
                LootData lootItem = lootItemDataList[j];
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

    private Dictionary<string, int> GetDefaultItems(LootSelectionSD lootSelectionSD) {
        var lootItemDataList = lootSelectionSD.LootItemDataList;
        Dictionary<string, int> itemDict = new(lootItemDataList.Count);
        for (int i = 0; i < lootItemDataList.Count; i++) {
            LootData lootItemData = lootItemDataList[i];
            string itemID = lootItemData.ItemSD.ID;
            itemDict.TryAdd(itemID, lootItemData.MinAppearence);
        }

        return itemDict;
    }
}
