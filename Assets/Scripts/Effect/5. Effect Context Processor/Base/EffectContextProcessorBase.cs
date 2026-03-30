using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class EffectContextProcessorBase
{
    public abstract void ApplyEffect(EffectContextBase effectContext);
}

public abstract class EffectContextProcessorBase<TEffectContext> : EffectContextProcessorBase
    where TEffectContext : EffectContextBase
{
    public override void ApplyEffect(EffectContextBase effectContext) {
        if (effectContext is TEffectContext castContext) {
            ApplyEffect(castContext);
        }
    }

    public abstract void ApplyEffect(TEffectContext effectContext);
}
