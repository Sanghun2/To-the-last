using System;
using BilliotGames;
using UnityEngine;

public class WeightCounter : IPushCondition
{
    public int CurrentWeight => currentWeight;
    public int LimitWeight => limitWeight;
    public int RemainWeight => limitWeight - currentWeight;

    protected int currentWeight;
    protected int limitWeight;

    public event Action<int, int, int> OnWeightChanged; // current, limit, prev
    public event Action OnWeightOver;

    public WeightCounter(int limitWeight) {
        this.limitWeight = limitWeight;
    }

    public void SetLimitWeight(int limitWeight) {
        this.limitWeight = limitWeight;
        OnWeightChanged?.Invoke(currentWeight, limitWeight, currentWeight);
    }

    public void AddWeight(int weight) {
        int prevWeight = currentWeight;
        currentWeight += weight;
        OnWeightChanged?.Invoke(currentWeight, limitWeight, prevWeight);
        if (currentWeight > limitWeight) {
            OnWeightOver?.Invoke();
        }
    }

    public bool CanPush(ItemStack item) {
        int unitWeight = (item.ItemData as ExtendedItemData)?.Weight ?? 0;
        return RemainWeight >= unitWeight * item.Amount;
    }
    public int GetAllowedAmount(ItemStack item) {
        int unitWeight = (item.ItemData as ExtendedItemData)?.Weight ?? 0;
        if (unitWeight <= 0) return item.Amount;
        return Mathf.Min(item.Amount, RemainWeight / unitWeight);
    }
}