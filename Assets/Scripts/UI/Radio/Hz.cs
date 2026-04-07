using System;
using UnityEngine;

[Serializable]
public class Hz
{
    public float CurrentHz
    {
        get => _currentHz;
        private set
        {
            _currentHz = Mathf.Round(value * 10f) / 10f;
            OnHzChanged?.Invoke(_currentHz);
        }
    }

    [SerializeField] private float hzModifier = 0.05f;
    [SerializeField] private float defaultHz = 100f;

    private float _baseHz;
    private float _currentHz;

    public event Action<float> OnHzChanged;

    public void InitHz() {
        _baseHz = defaultHz;
        CurrentHz = defaultHz;
    }

    public void UpdateHz(float value, float _) {
        CurrentHz = _baseHz + value * hzModifier;
    }

    public float GetHzModifier() => hzModifier;
}