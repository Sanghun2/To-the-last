using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BilliotGames;
using UnityEngine;
using Random = UnityEngine.Random;


public class LootSelectActionContext : SelectActionContext
{
    public InventoryBase Inventory => inventory;

    private InventoryBase inventory;

    public LootSelectActionContext(SelectionDataBase selectionData, InventoryBase inventory) : base(selectionData, selectionData.RequireMinutes) {
        this.inventory = inventory;
    }
}

public class LootSelectActionConverter : SelectActionConverter
{
    // 다른 위치로 이전
    public bool TryProcess(LootSelectionData lootData, SelectionContextBase context) {
        var lootContext = (LootSelectionContext)context;

        // 기본 아이템
        var itemDict = GetDefaultItems(lootData);

        // 랜덤 획득
        itemDict = GetAdditionalItems(itemDict, lootData, lootContext);


        // 추가 보상 조정

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

            if (!lootContext.TargetInventory.TryPushItem(new ItemStack(new ItemData(itemID, 999), amount), out var overflowedStack)) {
                Debug.Log($"push item inventory({lootContext.TargetInventory.InventoryID}) failed");
            }
        }

        return true;
    }
    private Dictionary<string, int> GetDefaultItems(LootSelectionData lootSelectionData) {
        var lootItemDataList = lootSelectionData.AvailableItemList;
        Dictionary<string, int> itemDict = new(lootItemDataList.Count);
        for (int i = 0; i < lootItemDataList.Count; i++) {
            LootData lootItemData = lootItemDataList[i];
            string itemID = lootItemData.ItemSD.ID;
            itemDict.TryAdd(itemID, lootItemData.MinAppearence);
        }

        return itemDict;
    }
    private Dictionary<string, int> GetAdditionalItems(
        Dictionary<string, int> itemDict,
        LootSelectionData lootSelectionData,
        LootSelectionContext context) {

        var lootItemDataList = lootSelectionData.AvailableItemList;
        var weightSum = lootItemDataList.Sum(data => data.Weight);
        int lootCount = (int)(lootSelectionData.DefaultLootCount * context.LootCountMutiflier);
        Debug.Log($"loot count: {lootCount}, weight: {weightSum}");

        for (int l = 0; l < lootCount; l++) {
            int targetWeight = Random.Range(1, weightSum + 1);
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

    private void Loot(LootSelectActionContext lootContext) {
        var targetInven = lootContext.Inventory;
        LootSelectionData lootSD = (LootSelectionData)lootContext.SelectionData;

        Guid? targetButtonID = Managers.SelectActionPipeline.CurrentSelectedButton.ButtonGuid;
        FocusJob job = new FocusJob(lootContext.JobDuration,
            onProgressChanged: Managers.SelectActionPipeline.CurrentSelectedButton.UpdateProcessUI,
            onComplete: () => {
                var selectionContext = new LootSelectionContext(targetInven).SetLootCountMultiflier(1);
                if (!TryProcess(lootSD, selectionContext)) {
                    Debug.LogError($"<color=red>{GetType()} select process failed</color>");
                }
            });

        Managers.Job.DoFocusJob(job, () => {
            Managers.SelectActionPipeline.ClearButton(targetButtonID);
        });
    }


    public override bool TryConvertAction(SelectActionContext context, out ActionData actionData) {
        if (context is LootSelectActionContext lootContext) {
            actionData = new ActionData(() => Loot(lootContext));
            return true;
        }

        Debug.LogError($"({context.GetType()})은 loot select action context로 변환 불가");
        actionData = null;
        return false;
    }
    public override bool TryProcess(SelectionContextBase selectionContext) {
        throw new NotImplementedException();
    }


    
}