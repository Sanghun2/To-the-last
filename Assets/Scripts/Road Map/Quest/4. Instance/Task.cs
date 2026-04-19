using System;
using UnityEngine;

public class Task
{
    public enum CountType
    {
        Overflow,
        Clamped,
    }
    public enum State {
        Wait,
        InProgress,
        Completed,
    }
    public int CurrentCount => currentCount;
    public int RequiredCount => requiredCount;
    public State CurrentState
    {
        get => _currentState;
        set
        {
            var prevState = _currentState;
            _currentState = value;
            if (_currentState != prevState) {
                OnStateChanged?.Invoke(this, _currentState);
            }
        }
    }

    [SerializeField] TaskData data;
    [SerializeField] protected int currentCount;
    [SerializeField] protected int requiredCount;
    private State _currentState;

    public event Action<Task, int, int> OnCountChanged; 
    //public event Action<Task> OnTaskCompleted;
    public event Action<Task, State> OnStateChanged;

    public Task(TaskInfo taskInfo) {
        this.data = taskInfo.TaskSD.ToData();
        currentCount = 0;
        this.requiredCount = taskInfo.RequiredCount;
    }

    public bool TryAddCount(int count) {
        if (count <= 0) return false;

        int prevCount = currentCount;
        SetCount(currentCount + count);

        if (prevCount < requiredCount && currentCount >= requiredCount) {
            //OnTaskCompleted?.Invoke(this);
            CurrentState = State.Completed;
        }

        return true;
    }
    public bool TryRemoveCount(int count) {
        if (count <= 0) return false;
        SetCount(currentCount - count);

        return true;
    }

    private void SetCount(int newCount) {
        newCount = data.CountType_ == CountType.Clamped
            ? Mathf.Clamp(newCount, 0, requiredCount)
            : Mathf.Max(0, newCount);

        if (newCount == currentCount) return;

        int prevCount = currentCount;
        currentCount = newCount;
        OnCountChanged?.Invoke(this, currentCount, prevCount);

    }

    public void Complete() {
        CurrentState = State.Completed;
    }
}
