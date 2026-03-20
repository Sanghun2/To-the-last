using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class SimpleInventory : InventoryBase
{
    public IReadOnlyList<ItemStack> ItemList => itemList;

    [SerializeField] List<ItemStack> itemList;
    protected Dictionary<string, int> itemCountDict = new Dictionary<string, int>();

    public override event Action<ItemStack> OnItemAdded;

    public SimpleInventory(string id, int capacitiy) : base(id, capacitiy) {
        InitInventory();
    }

    public override void InitInventory() {
        if (isInit) return;

        itemList = new List<ItemStack>(Capacity);

        isInit = true;
    }
    public override void ClearInventory() {
        itemList.Clear();
    }

    public override int GetItemCount(string itemID) {
        if (itemCountDict.TryGetValue(itemID, out int count)) {
            return count;
        }

        return 0;
    }
    public override bool TryPushItem(ItemStack inputStack, out ItemStack overflowedStack) {
        string itemID = inputStack.ItemData.ItemID;
        int inputCount = inputStack.Amount;
        int itemCount = GetItemCount(itemID);
        if (itemCount > 0) {
            overflowedStack = null;
            var targetStack = itemList.Find(item => item.ItemData.ItemID.Equals(inputStack.ItemData.ItemID));
            if (targetStack != null) {
                switch (targetStack.MergeStack(inputStack)) {
                    case ItemStack.MergeResult.Success:
                        OnItemAdded?.Invoke(inputStack);
                        break;
                    case ItemStack.MergeResult.Success_Overflowed:
                        int mergedCount = inputCount - inputStack.Amount;
                        ItemStack deltaStack = new ItemStack(inputStack.ItemData, mergedCount);
                        OnItemAdded?.Invoke(inputStack);
                        overflowedStack = inputStack;
                        break;
                    case ItemStack.MergeResult.Failed_DifferentItemType:
                    case ItemStack.MergeResult.Failed_InvalidIStack:
                    default:
                        return false;
                }
            }

            return true;
        }
        else {
            itemList.Add(inputStack);
            int resultAmount = Mathf.Min(inputStack.Amount, inputStack.ItemData.MaxStackAmount);
            overflowedStack = new ItemStack(inputStack.ItemData, inputStack.Amount - resultAmount);
            itemCountDict[itemID] = resultAmount;
            inputStack.OnAmountChanged -= UpdateItemCount;
            inputStack.OnAmountChanged += UpdateItemCount;
            var invenUI = Managers.UI.GetUI<InventoryUI>();
            if (invenUI.IsOpened) {
                invenUI.ShowInventory(this);
            }

            OnItemAdded?.Invoke(inputStack);
            return true;
        }
    }
    public override bool TryRemoveItem(string itemID, int targetAmount) {
        int itemCount = GetItemCount(itemID);
        if (itemCount >= targetAmount) {
            int index = itemList.FindIndex(item => item.ItemData.ItemID.Equals(itemID));
            ItemStack targetItem = itemList[index];
            if (targetItem.TryRemoveStack(targetAmount)) {
                if (targetItem.IsNull) {
                    itemList.RemoveAt(index);
                }
                return true;
            }

            Debug.LogError($"<color=red>has enough amount. but, failed to remove. current count: {itemCount}, request count: {targetAmount}</color>");
            return false;
        }

        Debug.LogAssertion($"not enough amount: require -> {targetAmount}, current: {itemCount}");
        return false;
    }



    private void UpdateItemCount(ItemStack itemStack, int deltaAmount) {
        string itemID = itemStack.ItemData.ItemID;
        if (itemCountDict.ContainsKey(itemID)) {
            itemCountDict[itemID] += deltaAmount;
        }
    }
}
