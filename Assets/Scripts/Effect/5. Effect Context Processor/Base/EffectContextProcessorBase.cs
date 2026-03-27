using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class EffectContextProcessorBase
{
    public abstract UniTask ApplyEffect(EffectContextBase effectContext);
}

public abstract class EffectContextProcessorBase<TEffectContext> : EffectContextProcessorBase
    where TEffectContext : EffectContextBase
{
    public override UniTask ApplyEffect(EffectContextBase effectContext) {
        if (effectContext is TEffectContext castContext) {
            return ApplyEffect(castContext);
        }

        return UniTask.CompletedTask;
    }

    public abstract UniTask ApplyEffect(TEffectContext effectContext);
}
