
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskSD", menuName = "Scriptable Objects/Quest/TaskSD")]
public class TaskSD : SDBase
{
    public Task.CountType CountType => countType;
    public Task.Temp_CompleteConditionType CompleteConditionType => completeConditionType;   

    [SerializeField] Task.CountType countType;
    [SerializeField] Task.Temp_CompleteConditionType completeConditionType;
}
