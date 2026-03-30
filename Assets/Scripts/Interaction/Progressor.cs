using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Progressor : IValue<float>
{
    public float Rate => currentValue / maxValue;
    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;

    private float currentValue;
    private float maxValue;

    public void Update(float currentValue, float maxValue) {
        this.currentValue = currentValue;
        this.maxValue = maxValue;
    }

    public void SetCurrentValue(float value) {
        currentValue = value;
    }
}
