using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class TempInventory : InventoryBase
{
    [SerializeField] List<ItemStack> itemList = new List<ItemStack>(100);
    protected Dictionary<string, int> itemCountDict = new Dictionary<string, int>();

    public override void InitInventory() {
        if (isInit) return;

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
        int itemCount = GetItemCount(itemID);
        if (itemCount != 0) {
            overflowedStack = null;
            var targetStack = itemList.Find(item => item.ItemData.ItemID.Equals(inputStack.ItemData.ItemID));
            if (targetStack != null) {
                switch (targetStack.MergeStack(inputStack)) {
                    case ItemStack.MergeResult.Success:
                        break;
                    case ItemStack.MergeResult.Success_Overflowed:
                        overflowedStack = inputStack;
                        break;
                    case ItemStack.MergeResult.Failed_DifferentItemType:
                        break;
                    case ItemStack.MergeResult.Failed_InvalidIStack:
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
        else {
            itemList.Add(inputStack);
            int resultAmount = Mathf.Min(inputStack.Amount, inputStack.ItemData.MaxStackAmount);
            overflowedStack = new ItemStack(inputStack.ItemData, inputStack.Amount - resultAmount);
            itemCountDict[itemID] = resultAmount;
            inputStack.OnAmountChanged += UpdateItemCount;
            return true;
        }
    }
    public override bool TryRemoveItem(string itemID, int targetAmount) {
        int itemCount = GetItemCount(itemID);
        if (itemCount >= targetAmount) {
            ItemStack targetItem = itemList.Find(item => item.ItemData.ItemID.Equals(itemID));
            if (targetItem.TryRemoveStack(targetAmount)) {
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
