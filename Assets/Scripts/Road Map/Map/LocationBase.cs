using System;
using BilliotGames;
using UnityEngine;

public abstract class LocationBase : IEquatable<LocationBase>
{
    public enum LocationState
    {
        Inactive,
        Active,
        Completed,
    }

    public string LocationUID => data.LocationUID;
    public string StoryDescription => data.StoryDescription;
    public string LocationName => data.DisplayName;
    public Vector2 AnchoredPosition => data.AnchoredPosition;
    public LocationState CurrentState
    {
        get => _currentState;
        protected set
        {
            var prevState = _currentState;
            _currentState = value;
            if (prevState != _currentState) {
                OnLocationStateChanged?.Invoke(value, prevState);
            }
        }
    }

    public LocationData Data => data;
    public event Action<LocationState, LocationState> OnLocationStateChanged;

    public InventoryBase Inventory
    {
        get
        {
            if (_inventory == null) {
                if (Managers.Inventory.TryGetInventoryByID(LocationUID, out var inven)) {
                    _inventory = inven as SimpleInventory;
                }
                else {
                    _inventory = new SimpleInventory($"{LocationUID}", 50);
                    Managers.Inventory.AddInventory(_inventory);
                }
            }

            return _inventory;
        }
    }
    private LocationState _currentState;
    private LocationData data;
    [SerializeField] SimpleInventory _inventory;

    public LocationBase(LocationData data) {
        this.data = data;
    }

    public bool Equals(LocationBase other) {
        if (this == null || other == null) return false;

        return LocationUID.Equals(other.LocationUID);
    }

    public LocationBase Activate() {
        CurrentState = LocationState.Active;
        return this;
    }
    public LocationBase Deactivate() {
        CurrentState = LocationState.Inactive;
        return this;
    }
    public virtual void ClearLocationEvent() {
        OnLocationStateChanged = null;
    }
}
