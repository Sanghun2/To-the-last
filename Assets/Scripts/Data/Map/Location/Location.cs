using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Location : IValue<int>, IEquatable<Location>
{
    public enum State {
        Undiscovered,
        Exploring,
        Completed,
    }

    public int CurrentValue => currentProgress;
    public int MaxValue => maxProgress;
    public State CurrentState
    {
        get => _currentState;
        protected set
        {
            var prevState = _currentState;
            _currentState = value;
            if (prevState != _currentState) {
                OnStateChanged?.Invoke(value, prevState);
            }
        }
    }
    public LocationData Data => data;

    public InventoryBase Inventory
    {
        get
        {
            if (_inventory == null) {
                _inventory = new SimpleInventory($"{locationID}", 20);
            }

            return _inventory;
        }
    }

    [SerializeField][HideInInspector] string locationID;
    [SerializeField] int currentProgress;
    [SerializeField] int maxProgress;
    [SerializeField] State _currentState;
    [SerializeField] SimpleInventory _inventory;

    [NonSerialized] private LocationData data;

    public Location(LocationData locationData) {
        this.data = locationData;
        locationID = locationData.LocationID;
        _currentState = State.Undiscovered;
    }

    public event Action<int, int> OnProgressChanged;
    public event Action<State, State> OnStateChanged;

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
            CurrentState = State.Completed;
        }
    }

    public Location Activate() {
        CurrentState = State.Exploring;
        return this;
    }
    public Location Deactivate() {
        CurrentState = State.Undiscovered;
        return this;
    }

    public void ClearLocationEvent() {
        OnStateChanged = null;
        OnProgressChanged = null;
    }

    public bool Equals(Location other) {
        if (this == null || other == null) return false;

        return locationID.Equals(other.locationID);
    }
}
