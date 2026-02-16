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

    [SerializeField] string locationID;
    [SerializeField] int currentProgress;
    [SerializeField] int maxProgress;
    [SerializeField] State _currentState;
    private LocationSD locationSD;

    public Location(LocationSD locationSD) {
        this.locationSD = locationSD;
        _currentState = State.Undiscovered;
    }

    public event Action<int, int> OnProgressChanged;
    public event Action<State, State> OnStateChanged;

    public void InitProgress(int current, int max) {
        currentProgress = current;
        maxProgress = max;
        OnProgressChanged?.Invoke(current, max);
    }
    public void ChangeProgress(int deltaValue) {
        currentProgress += deltaValue;
        OnProgressChanged?.Invoke(currentProgress, maxProgress);

        if (currentProgress == maxProgress) {
            CurrentState = State.Completed;
        }
    }

    public void Activate() {
        CurrentState = State.Exploring;
    }
    public void Deactivate() {
        CurrentState = State.Undiscovered;
    }

    public void ClearEvent() {
        OnStateChanged = null;
        OnProgressChanged = null;
    }
}
