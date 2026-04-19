using System;
using UnityEngine;

[Serializable]
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
        CanComplete,
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
                Debug.Log($"task state? {_currentState}");
            }
        }
    }

    private bool CanComplete => currentCount >= requiredCount;

    [SerializeField] TaskData data;
    [SerializeField] protected int currentCount;
    [SerializeField] protected int requiredCount;
    [SerializeField] State _currentState;
    private ITaskCompleteCondition taskCompleteCondition;

    public event Action<Task, int, int> OnCountChanged; 
    //public event Action<Task> OnTaskCompleted;
    public event Action<Task, State> OnStateChanged;

    public Task(TaskInfo taskInfo) {
        data = taskInfo.TaskSD.ToData();
        currentCount = 0;
        requiredCount = taskInfo.RequiredCount;
        CurrentState = State.Wait;
    }

    public bool TryComplete() {
        if (CurrentState != State.CanComplete) return false;

        Debug.Log($"<color=cyan>complete process action context required</color>");
        if (taskCompleteCondition != null && !taskCompleteCondition.Execute()) {
            return false;
        }

        CurrentState = State.Completed;
        return true;
    }
    public void StartTask() {
        CurrentState = State.InProgress;
    }


    public bool TryAddCount(int count) {
        if (count <= 0) return false;
        if (CurrentState == State.Wait) return false;

        int prevCount = currentCount;
        SetCount(currentCount + count);

        if (prevCount < requiredCount && CanComplete) {
            //OnTaskCompleted?.Invoke(this);
            CurrentState = State.CanComplete;
        }

        return true;
    }
    public bool TryRemoveCount(int count) {
        if (count <= 0) return false;
        int prevCount = currentCount;
        SetCount(currentCount - count);

        if (prevCount >= requiredCount && CurrentState == State.CanComplete) {
            CurrentState = State.InProgress;
        }

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
}
