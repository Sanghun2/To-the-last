using System.Collections.Generic;
using UnityEngine;

public class StatModifyEffectContextBuilder : EffectContextBuilderBase<StatModifyEffectData, StatModifyEffectContext>
{
    public override bool TryBuildContext(StatModifyEffectData effectData, Entity caster, IReadOnlyList<Entity> targets, out StatModifyEffectContext effectContext) {
        effectContext = new StatModifyEffectContext(effectData);
        effectContext.SetApplyContext(caster, targets);
        return true;
    }
}
