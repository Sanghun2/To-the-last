using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

        Debug.Log($"<color=cyan>({caster.EntityID}) Skill({skillSD.DisplayText}) 사용!</color>");

        var animationType = skillSD.AnimationType;
        caster.CurrentState = new AttackState(animationType, ExecuteEffects, () => {
            caster.CurrentState = new IdleState();
            onResolveCompleted?.Invoke();
        });
    }

    private void ExecuteEffects() {
        Debug.Log("effect applied");
        IReadOnlyList<Effect> effects = skillSD.Effects;
        for (int i = 0; i < effects.Count; i++) {
            var effect = effects[i];
            Managers.Effect.ApplyEffect(effect);
        }
    }
}
