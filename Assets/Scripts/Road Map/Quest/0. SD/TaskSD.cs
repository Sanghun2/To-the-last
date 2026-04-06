
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskSD", menuName = "Scriptable Objects/Quest/TaskSD")]
public class TaskSD : SDBase
{
    public Task.CountType CountType => countType;


    [SerializeField] Task.CountType countType;
}
