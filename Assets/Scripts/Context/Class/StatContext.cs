using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class StatContext : IStatContext, IApplyContext
{
    public Define.Stat TargetStat => targetStat;
    public StatContainer Stats => stats;
    public float ModifyingValue => modifyingValue;
    public Effect.OperatorType OperatorType => operatorType;


    public Entity Caster => caster;
    public IReadOnlyList<Entity> Targets => targets;

  
    private Define.Stat targetStat;
    private StatContainer stats;
    private float modifyingValue;
    private Effect.OperatorType operatorType;

    private Entity caster;
    private IReadOnlyList<Entity> targets;

    public StatContext SetStatInfo(
        Define.Stat targetStat, 
        StatContainer stats, 
        float modifyingValue, 
        Effect.OperatorType operatorType
        ) {
        this.targetStat = targetStat;
        this.stats = stats;
        this.modifyingValue = modifyingValue;
        this.operatorType = operatorType;
        return this;
    }

    public StatContext SetTargetInfo(Entity caster, IReadOnlyList<Entity> targets) {
        this.caster = caster;
        this.targets = targets;
        return this;
    }
}
