using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectContextBuilderBase
{
    public abstract bool TryBuildContext(EffectDataBase effectData, Entity caster, IReadOnlyList<Entity> targets, out EffectContextBase effectContext);
}

public abstract class EffectContextBuilderBase<TData, TContext> : EffectContextBuilderBase
    where TData : EffectDataBase
    where TContext : EffectContextBase
{
    public override bool TryBuildContext(EffectDataBase effectData, Entity caster, IReadOnlyList<Entity> targets, out EffectContextBase effectContext) {
        if (effectData is TData targetData) {
            var result = TryBuildContext(targetData, caster, targets, out TContext context);
            effectContext = context;
            return result;
        }

        Debug.LogError($"<color=red>effect context is not type of ({typeof(TData)})</color>");
        effectContext = null;
        return false;
    }

    public abstract bool TryBuildContext(TData effectData, Entity caster, IReadOnlyList<Entity> targets, out TContext effectContext);
}
