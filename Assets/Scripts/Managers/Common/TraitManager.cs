using System;
using UnityEngine;

public sealed class TraitManager
{
    public int AvailableTraitPoint
    {
        get => _availableTraitPoint;
        set
        {
            _availableTraitPoint = value;
            OnTraitPointChanged?.Invoke(_availableTraitPoint);
            Debug.Log($"point changed: {_availableTraitPoint}");
        }
    }

    private int _availableTraitPoint;

    public event Action<int> OnTraitPointChanged;


    public void InitTraitPoint() {
        AvailableTraitPoint = Managers.Player.PlayerData.GetAvailableTraitPoint();
    }
    public void ChangeTraitPoint(int point) {
        AvailableTraitPoint += point;
    }
}
