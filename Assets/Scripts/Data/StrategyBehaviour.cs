using System;
using UnityEngine;

public abstract class StrategyBehaviour
{
    public int BehaviourSpeed => behaviourSpeed;

    private int behaviourSpeed;
    private BattleEntity owner;
    private BattleEntity target;

    internal abstract void Resolve(Action onResolveCompleted=null);
}
