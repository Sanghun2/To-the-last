using UnityEngine;

public class TaskData : BaseData
{

    public int RequireCount => requireCount;
    public Task.CountType CountType_ => countType;

    [SerializeField] protected int requireCount;
    [SerializeField] Task.CountType countType;

    public TaskData(string id, Task.CountType countType, int requireCount) : base(id) {
        this.countType = countType;
        this.requireCount = requireCount;
    }
}
