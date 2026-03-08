using System;
using UnityEngine;

public sealed class SkillBehaviour : StrategyBehaviour
{
    private SkillSD skillSD;
    // animation

    public SkillBehaviour(BattleEntity caster, SkillSD skillSD) : base(
            caster, 
            skillSD.BehaviourType,
            (int)BattleUtility.CalculateBehaviourSpeed(caster)
            ) 
        {
        this.skillSD = skillSD;
    }

    public override void Resolve(Action onResolveCompleted = null) {
        if (skillSD == null) { 
            Debug.LogError($"<color=red>skill data is null</color>");
            onResolveCompleted?.Invoke();
            return; 
        }

        var effects = skillSD.Effects;
        for (int i = 0; i < effects.Count; i++) {
            var effect = effects[i];
            EffectProcessor processor = Managers.BattleSystem.GetEffectProcessor(effect);
            processor.ProcessEffect(effect, caster);
        }

        onResolveCompleted?.Invoke();
    }
}
