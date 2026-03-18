using BilliotGames;
using UnityEngine;

public abstract class TimeBasedSD : ImageSDBase
{
    public int RequireMinutes => requireMinutes;

    [SerializeField] protected int requireMinutes;
}
