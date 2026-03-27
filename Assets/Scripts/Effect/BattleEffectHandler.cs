using UnityEngine;

public class BattleEffectHandler : IEffectHandler<BattleContext>
{
    public void Execute(BattleContext context) {
        var effect = context.EffectSD;
        var caster = context.Caster;
        var targets = context.Targets;

        if (effect == null) { Debug.LogError($"<color=red>process failed. effect is null</color>"); return; }
        if (caster == null) { Debug.LogError($"<color=red>effect caster shouldn't be null</color>"); return; }

        var target = targets[0];
        target = Managers.BattleSystem.ResolveTarget(caster, effect.TargetType);
        Debug.Log($"<color=orange>caster id? ({caster.EntityID}), target id? ({target?.EntityID})</color>");
        effect.ApplyEffect(caster, target);
    }

    public void Execute(IContext context) {
        if (context is BattleContext battleContext) {
            Execute(battleContext);
        }
        else {
            Debug.LogError($"({context.GetType()}) is not type of ({typeof(BattleContext)})");
        }
    }
}
