
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskSD", menuName = "Scriptable Objects/Quest/TaskSD")]
public class TaskSD : SDBase
{
    public Task.CountType CountType => countType;
    public int RequireCount => requireCount;    


    [SerializeField] Task.CountType countType;
    [SerializeField] int requireCount;
}
