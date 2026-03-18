using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusSD", menuName = "Scriptable Objects/StatusSD")]
public class StatusSD : ImageSDBase
{
    public int MaxStack => maxStack;

    [SerializeField] int maxStack;
}
