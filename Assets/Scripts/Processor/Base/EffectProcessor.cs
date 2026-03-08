using UnityEngine;


public abstract class EffectProcessor
{
    public abstract void ProcessEffect(IEffect effect, Entity caster, Entity target = null);
}
public abstract class EffectProcessor<TEffect> : EffectProcessor where TEffect : IEffect
{
    public abstract void ProcessEffect(TEffect effect, Entity caster, Entity target=null);
}
