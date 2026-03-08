using UnityEngine;

public class EffectSDProcessor : EffectProcessor<EffectSD>
{
    public override void ProcessEffect(EffectSD effect, Entity caster, Entity target = null) {
        if (effect == null) { Debug.LogError($"<color=red>process failed. effect is null</color>"); return; }
        if (caster == null) { Debug.LogError($"<color=red>effect caster shouldn't be null</color>"); return; }

        target = Managers.BattleSystem.ResolveTarget(caster, effect.TargetType);
        Debug.Log($"<color=orange>caster id? ({caster.EntityID}), target id? ({target?.EntityID})</color>");
        effect.ApplyEffect(caster, target);
    }

    public override void ProcessEffect(IEffect effect, Entity caster, Entity target = null) {
        ProcessEffect(effect as EffectSD, caster, target);
    }
}
