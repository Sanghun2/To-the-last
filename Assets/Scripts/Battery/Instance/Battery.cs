using System;
using UnityEngine;

[Serializable]
public class Battery
{
    public bool IsEmpty => currentValue <= minValue;
    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;


    [SerializeField] float consumeRatePerMinutes = 0.0139f;
    [SerializeField] float consumeRatePerRotation = 0.0139f;
    [SerializeField] float startValue = 100;
    [SerializeField] float maxValue = 100;
    [SerializeField] float currentValue;
    private float minValue = 0;

    public event Action<float, float> OnValueChanged;

    public virtual void Init() {
        currentValue = startValue;
        OnValueChanged?.Invoke(currentValue, maxValue);
    }

    public void ChangeValue(float delta) {
        currentValue = Mathf.Clamp(currentValue + delta, minValue, maxValue);
        OnValueChanged?.Invoke(currentValue, maxValue);
    }

    public void ConsumeValue(int day, int hour, int minute, int deltaMinutes) {
        ChangeValue(deltaMinutes * -consumeRatePerMinutes);
    }

    public void ConsumeValue(float _, float deltaValue) {
        var absValue = Mathf.Abs(deltaValue);
        ChangeValue(absValue * -consumeRatePerRotation);
    }
}
