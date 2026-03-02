using System;
using UnityEngine;

public abstract class StrategyBehaviour
{
    public int Priority => priority;

    private int priority;
    private BattleEntity owner;
    private BattleEntity target;

    internal abstract void Resolve(Action onResolveCompleted=null);
}
