using System.Collections.Generic;
using UnityEngine;

public class StatModifyEffectContext : EffectContextBase, IApplyContext
{
    public new StatModifyEffectData EffectData => effectData;
    public Entity Caster => caster;
    public IReadOnlyList<Entity> Targets => targets;


    private StatModifyEffectData effectData;
    private IReadOnlyList<Entity> targets;
    private Entity caster;



    public StatModifyEffectContext(StatModifyEffectData effectData) {
        this.effectData = effectData;
    }

    public void SetApplyContext(Entity caster, IReadOnlyList<Entity> targets) {
        this.caster = caster;
        this.targets = targets;
    }
}