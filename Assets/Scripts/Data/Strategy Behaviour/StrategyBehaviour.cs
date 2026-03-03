using System;
using UnityEngine;

public abstract class StrategyBehaviour : IComparable<StrategyBehaviour>
{
    public enum BehaviourType {
        Initiaive,
        Counter,
        Normal,
    }

    public int BehaviourSpeed => behaviourSpeed;

    private int behaviourSpeed;
    private BehaviourType behaviourType;
    protected BattleEntity owner;
    protected BattleEntity target;

    public StrategyBehaviour(BattleEntity owner, BehaviourType behaviourType, int behaviourSpeed) {
        this.owner = owner; 
        this.behaviourType = behaviourType;
        this.behaviourSpeed = behaviourSpeed;
    }

    internal abstract void Resolve(Action onResolveCompleted=null);

    public int CompareTo(StrategyBehaviour other) {
        if (other == null) return 1;
        int result = -(behaviourType.CompareTo(other.behaviourType));
        if (result == 0) {
            return behaviourSpeed.CompareTo(other.behaviourSpeed);
        }

        return result;
    }
}
