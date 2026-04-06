using System;
using UnityEngine;

public class Task
{
    public enum CountType
    {
        Overflow,
        Clamped,
    }
    public int CurrentCount => currentCount;
    public int RequiredCount => requiredCount;

    [SerializeField] TaskData data;
    [SerializeField] protected int currentCount;
    [SerializeField] protected int requiredCount;

    public event Action<Task, int, int> OnCountChanged; 
    public event Action<Task> OnTaskCompleted;

    public Task(TaskInfo taskInfo) {
        this.data = taskInfo.TaskSD.ToData();
        currentCount = 0;
        this.requiredCount = taskInfo.RequiredCount;
    }

    public void AddCount(int count) {
        if (count <= 0) return;

        int prevCount = currentCount;
        SetCount(currentCount + count);

        if (prevCount < requiredCount && currentCount >= requiredCount)
            OnTaskCompleted?.Invoke(this);
    }
    public void RemoveCount(int count) {
        if (count <= 0) return;
        SetCount(currentCount - count);
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
