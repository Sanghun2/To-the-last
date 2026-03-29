using System;
using BilliotGames;
using UnityEngine;

public class WeightCounter
{
    public int CurrentWeight => currentWeight;
    public int LimitWeight => limitWeight;

    public int RemainWeight => limitWeight - currentWeight;

    protected int currentWeight;
    protected int limitWeight;

    public event Action<int, int, int> OnWeightChanged;
    public event Action OnWeightOver;

    public WeightCounter(int limitWeight) {
        SetWeight(limitWeight);
    }

    public void SetWeight(int limitWeight) {
        this.limitWeight = limitWeight;
        OnWeightChanged?.Invoke(currentWeight, limitWeight, currentWeight);
    }
    public bool CanAddWeight(int weight) {
        return (currentWeight + weight) <= limitWeight;
    }
    public void AddWeight(int weight) {
        int prevWeight = currentWeight;
        currentWeight += weight;

        OnWeightChanged?.Invoke(currentWeight, limitWeight, prevWeight);

        if (currentWeight > limitWeight) {
            OnWeightOver?.Invoke();
        }
    }
}
