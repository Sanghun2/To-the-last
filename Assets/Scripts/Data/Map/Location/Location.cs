using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Location : IValue<int>, IEquatable<Location>
{
    public enum LocationState {
        Undiscovered,
        Exploring,
        Completed,
    }

    public int CurrentValue => currentProgress;
    public int MaxValue => maxProgress;
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
    public string ID => data.LocationID;
    public string DisplayText => data.DisplayText;
    public LocationData Data => data;

    public InventoryBase Inventory
    {
        get
        {
            if (_inventory == null) {
                if (Managers.Inventory.TryGetInventoryByID(locationID, out var inven)) {
                    _inventory = inven as SimpleInventory;
                }
                else {
                    _inventory = new SimpleInventory($"{locationID}", 50);
                    Managers.Inventory.AddInventory(_inventory);
                }
            }

            return _inventory;
        }
    }


    [SerializeField][HideInInspector] string locationID;
    [SerializeField] int currentProgress;
    [SerializeField] int maxProgress;
    [SerializeField] LocationState _currentState;
    [SerializeField] SimpleInventory _inventory;

    [NonSerialized] private LocationData data;

    public Location(LocationData locationData) {
        this.data = locationData;
        locationID = locationData.LocationID;
        _currentState = LocationState.Undiscovered;
    }

    public event Action<int, int> OnProgressChanged;
    public event Action<LocationState, LocationState> OnLocationStateChanged;

    public Location InitProgress(int current, int max) {
        currentProgress = current;
        maxProgress = max;
        OnProgressChanged?.Invoke(current, max);
        return this;
    }
    public void ChangeProgress(int deltaValue) {
        currentProgress += deltaValue;
        OnProgressChanged?.Invoke(currentProgress, maxProgress);

        if (currentProgress == maxProgress) {
            CurrentState = LocationState.Completed;
        }
    }

    public Location Activate() {
        CurrentState = LocationState.Exploring;
        return this;
    }
    public Location Deactivate() {
        CurrentState = LocationState.Undiscovered;
        return this;
    }

    public void ClearLocationEvent() {
        OnLocationStateChanged = null;
        OnProgressChanged = null;
    }

    public bool Equals(Location other) {
        if (this == null || other == null) return false;

        return locationID.Equals(other.locationID);
    }
}
