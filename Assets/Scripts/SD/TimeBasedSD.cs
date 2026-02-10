using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeBasedSD", menuName = "Scriptable Objects/TimeBasedSD")]
public abstract class TimeBasedSD : IconSDBase
{
    public int RequireMinutes => requireMinutes;

    [SerializeField] protected int requireMinutes;
}
