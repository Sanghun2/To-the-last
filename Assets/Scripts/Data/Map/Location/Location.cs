using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Location : IValue<int>
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
    public LocationSD LocationSD => locationSD;

    [SerializeField][HideInInspector] string locationID;
    [SerializeField] int currentProgress;
    [SerializeField] int maxProgress;
    [SerializeField] State _currentState;

    [NonSerialized] private LocationSD locationSD;

    public Location(LocationSD locationSD) {
        this.locationSD = locationSD;
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
}
