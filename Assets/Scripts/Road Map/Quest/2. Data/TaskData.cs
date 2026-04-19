using UnityEngine;

public sealed class TaskData : BaseData
{
    public Task.CountType CountType_ => countType;

    [SerializeField] Task.CountType countType;

    public TaskData(string id, Task.CountType countType) : base(id) {
        this.countType = countType;
    }
}
