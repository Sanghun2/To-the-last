using System;
using System.Text;
using BilliotGames;
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
    public enum Temp_CompleteConditionType
    {
        None,
        ConsumeItem,
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
    public readonly string EventKey;


    [SerializeField] State _currentState;
    [SerializeField] TaskData data;
    [SerializeField] protected int currentCount;

    [SerializeField] string taskType;
    [SerializeField] SDBase taskTarget;
    [SerializeField] protected int requiredCount;

    private ITaskCompleteCondition taskCompleteCondition;

    public event Action<Task, int, int> OnCountChanged; 
    //public event Action<Task> OnTaskCompleted;
    public event Action<Task, State> OnStateChanged;

    public Task(TaskInfo taskInfo) {
        data = taskInfo.TaskSD.ToData();
        taskType = taskInfo.TargetSD.ID;
        taskTarget = taskInfo.TargetSD;
        currentCount = 0;
        requiredCount = taskInfo.RequiredCount;
        CurrentState = State.Wait;
        taskCompleteCondition = BuildCompleteCondition(data.CompleteConditionType);
        EventKey = BuildEventKey(taskType, taskTarget);
    }


    public bool TryComplete() {
        if (CurrentState != State.CanComplete) return false;

        // complete condition check
        Debug.Log($"complete condition? {taskCompleteCondition?.GetType()}");
        if (taskCompleteCondition != null && !taskCompleteCondition.TryPassCondition()) {
            Debug.LogError($"<color=orange>complete process action context required</color>");
            return false;
        }

        CurrentState = State.Completed;
        Managers.EventBus.UnregisterEvent<string, int, int>(EventKey, ChangeCount);
        return true;
    }
    public void StartTask() {
        if (CurrentState == State.InProgress) return;
        CurrentState = State.InProgress;
        Managers.EventBus.RegisterEvent<string, int, int>(EventKey, ChangeCount);
    }


    public void ChangeCount(string targetName, int currentCount, int delta) {
        var result = delta >= 0 ? TryAddCount(delta) : TryRemoveCount(-delta);
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


    private string BuildEventKey(string taskType, SDBase taskTaret) {
        StringBuilder keyBuilder = new StringBuilder();

        if (taskTarget is ItemSD) {
            keyBuilder.Append(Define.Event.GET_ITEM);
        }

        return keyBuilder.ToString();
    }
    private ITaskCompleteCondition BuildCompleteCondition(Temp_CompleteConditionType completeConditionType) {
        switch (completeConditionType) {
            case Temp_CompleteConditionType.None:
            default:
                return null;
            case Temp_CompleteConditionType.ConsumeItem:
                return new ConsumeItemCondition(
                    taskTarget.ID, 
                    requiredCount, 
                    InventoryUtility.GetInventoriesByCurrentLocation());
        }
    }

}
