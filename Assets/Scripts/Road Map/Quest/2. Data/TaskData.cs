using System;
using UnityEngine;

public sealed class TaskData : BaseData
{
    public Task.CountType CountType_ => countType;

    public Task.Temp_CompleteConditionType CompleteConditionType => completeConditionType;   

    [SerializeField] Task.CountType countType;
    [SerializeField] Task.Temp_CompleteConditionType completeConditionType;

    public TaskData(string id, Task.CountType countType, Task.Temp_CompleteConditionType completeCondition) : base(id) {
        this.countType = countType;
        this.completeConditionType = completeCondition;
    }
}
