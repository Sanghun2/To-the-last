using System;
using UnityEngine;

[Serializable]
public class Battery
{
    public bool IsEmpty => currentValue <= minValue;
    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;


    [SerializeField] float consumeRatePerMinutes = 0.0139f;
    [SerializeField] float startValue = 100;
    [SerializeField] float maxValue = 100;
    private float minValue = 0;
    private float currentValue;

    public event Action<float, float> OnValueChanged;

    public virtual void Init() {
        currentValue = startValue;
        OnValueChanged?.Invoke(currentValue, maxValue);
    }

    public void ChangeValue(float delta) {
        if (currentValue + delta <= minValue) return;
        currentValue = Mathf.Clamp(currentValue + delta, minValue, maxValue);
        OnValueChanged?.Invoke(currentValue, maxValue);
    }

    internal void ConsumeValue(int day, int hour, int minute, int deltaMinutes) {
        ChangeValue(deltaMinutes * -consumeRatePerMinutes);
    }
}
