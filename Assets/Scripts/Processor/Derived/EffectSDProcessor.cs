using UnityEngine;

public class EffectSDProcessor : EffectProcessor<EffectSD>
{
    public override void ProcessEffect(EffectSD effect, Entity caster, Entity target = null) {
        if (effect == null) { Debug.LogError($"<color=red>process failed. effect is null</color>"); return; }
        if (caster == null) { Debug.LogError($"<color=red>effect caster shouldn't be null</color>"); return; }

        effect.ApplyEffect(caster, Managers.BattleSystem.ResolveTarget(caster, effect.TargetType));
    }

    public override void ProcessEffect(IEffect effect, Entity caster, Entity target = null) {
        ProcessEffect(effect as EffectSD, caster, target);
    }
}
