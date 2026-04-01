using BilliotGames;
using UnityEngine;

public abstract class TimeSDBase : ImageSDBase
{
    public int RequireMinutes => requireMinutes;

    [SerializeField] protected int requireMinutes;
}
