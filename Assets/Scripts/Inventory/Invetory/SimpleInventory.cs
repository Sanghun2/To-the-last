using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[Serializable]
public class SimpleInventory : InventoryBase
{
    public IReadOnlyList<ItemStack> ItemList
    {
        get
        {
            return itemList;
        }
    }
    public WeightCounter WeightCounter => weightCounter;

    [SerializeField] List<ItemStack> itemList = new List<ItemStack>();
    protected Dictionary<string, int> itemCountDict = new Dictionary<string, int>();
    private WeightCounter weightCounter = null;

    public override event Action<ItemStack, int> OnItemAdded;
    public override event Action<ItemStack, int> OnItemMerged;
    public override event Action<ItemStack, int> OnItemRemoved;

    public SimpleInventory(string id, int capacitiy = 15) : base(id, capacitiy) {
        InitInventory();
    }

    public override void InitInventory() {
        //Debug.Log($"InitInventory called. isInit={isInit}, itemList={itemList == null}");
        if (isInit) return;

        itemList = new List<ItemStack>(Capacity);
        itemCountDict = new Dictionary<string, int>();
        itemList.RemoveAll(item => item == null || item.ItemData == null);


        isInit = true;
    }

    public SimpleInventory SetWeightCounter(WeightCounter weightCounter) {
        this.weightCounter = weightCounter;

        OnItemAdded -= UpdateWeight;
        OnItemAdded += UpdateWeight;

        OnItemMerged -= UpdateWeight;
        OnItemMerged += UpdateWeight;

        OnItemRemoved -= UpdateWeight;
        OnItemRemoved += UpdateWeight;

        return this;
    }

    public override void ClearInventory() {
        itemList.Clear();
    }

    public override int GetItemCount(string itemID) {
        InitInventory();
        if (itemCountDict.TryGetValue(itemID, out int count)) {
            return count;
        }

        return 0;
    }
    public override bool TryPushItem(ItemStack inputStack, out ItemStack overflowedStack) {
        overflowedStack = null;

        // ── 무게 체크 ──────────────────────────────────────────────────────────
        // weightCounter가 null이면 이 인벤토리는 무게 제한 없음 (ex. location inventory)
        if (weightCounter != null) {
            // ExtendedItemData가 아닌 경우 무게 개념이 없으므로 unitWeight = 0으로 처리
            int unitWeight = (inputStack.ItemData as ExtendedItemData)?.Weight ?? 0;

            if (unitWeight > 0) {
                int remainWeight = weightCounter.RemainWeight;

                // 단 1개도 넣을 수 없으면 전량 overflow로 반환하고 실패
                if (remainWeight < unitWeight) {
                    Debug.LogAssertion("over weight");
                    overflowedStack = new ItemStack(inputStack.ItemData, inputStack.Amount);
                    return false;
                }

                // 무게 기준으로 넣을 수 있는 최대 수량 계산
                // ex. 남은무게=10, 단위무게=3 → 최대 3개
                int maxByWeight = remainWeight / unitWeight;

                if (inputStack.Amount > maxByWeight) {
                    // 초과분은 overflow로 분리, 넣을 수량만 새 스택으로 교체
                    // inputStack.Amount를 직접 수정하면 Amount setter에서 ReleaseItem이
                    // 호출될 수 있으므로 새 인스턴스로 교체
                    overflowedStack = new ItemStack(inputStack.ItemData, inputStack.Amount - maxByWeight);
                    inputStack = new ItemStack(inputStack.ItemData, maxByWeight);
                }
            }
        }

        // ── 초기화 보장 ────────────────────────────────────────────────────────
        // 무게 체크 이후에 호출하는 이유: 무게 체크는 itemList 접근이 필요 없으므로
        // 초기화 전에도 수행 가능하고, 실패 시 불필요한 초기화를 피하기 위함
        InitInventory();

        string inputItemID = inputStack.ItemData.ItemID;

        // 롤백 대비용 수량 스냅샷
        // MergeStack/Add 실패 시 원래 수량을 overflow로 돌려줘야 하므로 미리 저장
        int snapshotAmount = inputStack.Amount;

        // ── 기존 스택에 병합 ────────────────────────────────────────────────────
        // Amount < MaxStackAmount 조건: 이미 꽉 찬 스택은 병합 대상에서 제외
        var targetStack = itemList.Find(item =>
            item.ItemData.ItemID.Equals(inputItemID) &&
            item.Amount < item.ItemData.MaxStackAmount);

        if (targetStack != null) {
            int prevInputAmount = inputStack.Amount;
            int currentInputAmount = 0;

            switch (targetStack.MergeStack(inputStack)) {
                case ItemStack.MergeResult.Success:
                    // inputStack.Amount가 0이 됐으므로 실제 병합된 양 = prev - current
                    currentInputAmount = inputStack.Amount;
                    OnItemMerged?.Invoke(targetStack, prevInputAmount - currentInputAmount);
                    return true;

                case ItemStack.MergeResult.Success_Overflowed:
                    // targetStack이 꽉 찼고 inputStack에 잔량이 남은 경우
                    // 잔량을 다음 슬롯에 재귀적으로 밀어 넣음
                    currentInputAmount = inputStack.Amount;
                    OnItemMerged?.Invoke(targetStack, prevInputAmount - currentInputAmount);
                    return TryPushItem(inputStack, out overflowedStack);

                case ItemStack.MergeResult.Failed_DifferentItemType:
                case ItemStack.MergeResult.Failed_InvalidIStack:
                default:
                    // MergeStack 실패는 amount를 건드리지 않으므로 별도 롤백 불필요
                    return false;
            }
        }
        // ── 새 슬롯에 추가 ──────────────────────────────────────────────────────
        else {
            itemList.Add(inputStack);

            // capacity 초과 여부 확인
            // Add 후에 체크하는 이유: Add 전에는 Count가 아직 반영 안 됨
            if (itemList.Count > Capacity) {
                // 롤백: 방금 추가한 스택을 제거하고 전량 overflow로 반환
                itemList.RemoveAt(itemList.Count - 1);
                // 무게로 이미 잘린 overflowedStack이 있을 수 있으므로
                // 이번에 못 넣은 snapshotAmount도 합산해서 반환
                int existingOverflow = overflowedStack?.Amount ?? 0;
                overflowedStack = new ItemStack(inputStack.ItemData, snapshotAmount + existingOverflow);
                return false;
            }

            // MaxStackAmount 초과분을 overflow로 분리
            // ex. MaxStack=10, inputAmount=13 → resultAmount=10, overflow=3
            int resultAmount = Mathf.Min(inputStack.Amount, inputStack.ItemData.MaxStackAmount);

            // 무게로 잘린 overflow(이미 존재할 수 있음)와
            // MaxStack 초과로 잘린 overflow를 합산
            int existingOverflowAmount = overflowedStack?.Amount ?? 0;
            overflowedStack = new ItemStack(
                inputStack.ItemData,
                existingOverflowAmount + (inputStack.Amount - resultAmount));

            // itemCountDict 갱신: 실제로 들어간 수량(resultAmount)으로 등록
            itemCountDict[inputItemID] = resultAmount;

            // OnAmountChanged 이벤트 구독
            // 중복 구독 방지를 위해 먼저 제거 후 추가
            inputStack.OnAmountChanged -= UpdateItemCount;
            inputStack.OnAmountChanged += UpdateItemCount;

            var invenUI = Managers.UI.GetUI<InventoryUI>();
            if (invenUI.IsOpened) {
                invenUI.ShowInventory(this);
            }

            OnItemAdded?.Invoke(inputStack, inputStack.Amount);
            return true;
        }
    }
    public override bool TryRemoveItem(string itemID, int targetAmount) {
        InitInventory();
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
    public override int RemoveItemPartial(string itemID, int requestAmount) {
        InitInventory();
        int available = GetItemCount(itemID);
        int toRemove = Mathf.Min(available, requestAmount);
        if (toRemove <= 0) return 0;

        int remaining = toRemove;
        for (int i = itemList.Count - 1; i >= 0 && remaining > 0; i--) {
            var stack = itemList[i];
            if (!stack.ItemData.ItemID.Equals(itemID)) continue;

            int removeFromStack = Mathf.Min(stack.Amount, remaining);
            stack.TryRemoveStack(removeFromStack);
            remaining -= removeFromStack;

            if (stack.IsNull) {
                itemList.RemoveAt(i);
            }
        }

        OnItemRemoved?.Invoke(null, toRemove); // 필요에 따라 조정
        return toRemove;
    }


    private void UpdateItemCount(ItemStack itemStack, int deltaAmount) {
        string itemID = itemStack.ItemData.ItemID;
        if (itemCountDict.ContainsKey(itemID)) {
            itemCountDict[itemID] += deltaAmount;
        }
    }
    private void UpdateWeight(ItemStack stack, int deltaAmount) {
        if (stack.ItemData is ExtendedItemData data) {
            weightCounter.AddWeight(deltaAmount * data.Weight);
        }
    }
}
