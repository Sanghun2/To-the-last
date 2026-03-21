using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class StatModificationHandler : IEffectHandler<StatContext>
{
    public void Execute(StatContext context) {
        Define.Stat targetStat = context.TargetStat;
        StatContainer stats = context.Stats;
        float modifyingValue = context.ModifyingValue;
        Entity caster = context.Caster;
        var targets = context.Targets;
        Effect.OperatorType operatorType = context.OperatorType;

        ModifyStat(stats, targetStat, modifyingValue, operatorType, caster, targets);
    }

    public void Execute(IContext context) {
        Execute((StatContext)context);
    }

    private void ModifyStat(StatContainer stats, Define.Stat targetStat, float modifyingValue, Effect.OperatorType operatorType, Entity caster, System.Collections.Generic.IReadOnlyList<Entity> targets) {
        if (!IsValid(caster, targets)) return;

        // target 처리
        if (targets[0] is BattleEntity targetEntity) {
            if (!targetEntity.TryGetStatValue(targetStat, out float baseValue)) { Debug.LogError($"<color=red>target이 stat({targetStat})울 가지고 있지 않음</color>"); return; }

            var resultValue = Calculator.CalculateValue(baseValue, modifyingValue, operatorType);
            targetEntity.TryChangeStat(targetStat.ToID(), resultValue.deltaValue);

            ShowEffectAnimation(targetEntity, resultValue.deltaValue);

            // test
            targetEntity.TryGetStatValue(targetStat, out float viewValue);
            Debug.Log($"<color=yellow>[Test] ({targetEntity.EntityID})의 ({targetStat.ToID()}) ({resultValue.deltaValue}) 감소. 남은 체력:({viewValue})</color>");
            // ---
        }
        else {
            Debug.LogError($"<color=red>entity가 battle entity가 아니어서 stat을 변경할 수 없음</color>");
        }
    }

    private void ShowEffectAnimation(BattleEntity targetEntity, float deltaValue) {
        var battleUI = Managers.UI.GetUI<BattleUI>();
        EntityUI targetEntityUI = battleUI.GetEntityUI(targetEntity);


        var context = new FloatingTextContext(
            deltaValue.ToString(),
            targetEntityUI.transform.position,
            FloatingText.TextType.Damage
            );

        battleUI.FloatingText.ShowText(context);
    }

    protected bool IsValid(Entity caster, IReadOnlyList<Entity> targets) {
        if (caster == null || targets == null) { Debug.Log($"entity null. caster null? {caster == null}, target null? {targets == null}"); return false; }                

        return targets.Count > 0;
    }
}
