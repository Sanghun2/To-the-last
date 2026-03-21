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

        Debug.Log($"<color=cyan>({caster.EntityID}) Skill({skillSD.DisplayText}) 사용!</color>");

        var animationType = skillSD.AnimationType;
        caster.CurrentState = new AttackState(animationType, ExecuteEffects, () => {
            caster.CurrentState = new IdleState();
            onResolveCompleted?.Invoke();
        });
    }

    private void ExecuteEffects() {
        Debug.Log("effect applied");
        var effects = skillSD.Effects;
        for (int i = 0; i < effects.Count; i++) {
            var effect = effects[i];
            if (Managers.EffectSystem.TryGet(effect, out var handler)) {
                handler.Execute(new BattleContext(effect, caster)); // gettype으로 들고 오면 stat modifier 같은 애들을 processor가 없어서 effectHandler null error 발생
            }
            else {
                Debug.LogError($"<color=red>handler of ({effect.GetType()}) is null</color>");
            }
        }
    }
}
