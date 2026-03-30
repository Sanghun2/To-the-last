using System.Collections.Generic;
using UnityEngine;

public class StatModifyEffectContext : EffectContextBase, IApplyContext
{
    public new StatModifyEffectData Data => data;
    public Entity Caster => caster;
    public IReadOnlyList<Entity> Targets => targets;
    public Define.Stat TargetStat => data.TargetStat;
    public float Value => data.Value;


    private StatModifyEffectData data;
    private IReadOnlyList<Entity> targets;
    private Entity caster;



    public StatModifyEffectContext(StatModifyEffectData effectData) {
        this.data = effectData;
    }

    public void SetApplyContext(Entity caster, IReadOnlyList<Entity> targets) {
        this.caster = caster;
        this.targets = targets;
    }
}