using BilliotGames;
using UnityEngine;

public abstract class TimeBasedSD : IconSDBase
{
    public int RequireMinutes => requireMinutes;

    [SerializeField] protected int requireMinutes;
}
