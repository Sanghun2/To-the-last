using System;
using UnityEngine;

public abstract class StrategyBehaviour : IComparable<StrategyBehaviour>
{
    public enum BehaviourType {
        Initiative,  // 선공기
        Counter,    // 반격기
        Normal,     // 일반기
    }

    public enum TargetType {
        None,
        Self,
        ClosestEnemy,
    }

    public int BehaviourSpeed => behaviourSpeed;

    private int behaviourSpeed;
    private BehaviourType behaviourType;
    protected BattleEntity caster;
    protected BattleEntity target;

    public StrategyBehaviour(BattleEntity caster, BehaviourType behaviourType, int behaviourSpeed, BattleEntity target=null) {
        this.caster = caster; 
        this.target = target;
        this.behaviourType = behaviourType;
        this.behaviourSpeed = behaviourSpeed;
    }

    public abstract void Resolve(Action onResolveCompleted=null);

    public int CompareTo(StrategyBehaviour other) {
        if (other == null) return 1;
        int result = -(behaviourType.CompareTo(other.behaviourType));
        if (result == 0) {
            return behaviourSpeed.CompareTo(other.behaviourSpeed);
        }

        return result;
    }
    public void SetTarget(BattleEntity target) {
        this.target = target;
    }
}
