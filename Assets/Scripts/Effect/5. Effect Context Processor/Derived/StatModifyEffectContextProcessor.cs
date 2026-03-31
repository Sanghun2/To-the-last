using System.Collections.Generic;
using BilliotGames;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StatModifyEffectContextProcessor : EffectContextProcessorBase<StatModifyEffectContext>
{
    public override void ApplyEffect(StatModifyEffectContext effectContext) {
        IReadOnlyList<Entity> targets = ResolveTarget(effectContext);
        if (targets != null) {
            //targets
            for (int i = 0; i < targets.Count; i++) {
                var target = targets[i];

                if (target.Stats.TryGetStat(effectContext.TargetStat.ToID(), out IStatEntry stat)) {
                    stat.ChangeRawValue(effectContext.Value);
                }
            }
        }
        else {
            Debug.Log("target is null");
        }
    }

    private IReadOnlyList<Entity> ResolveTarget(StatModifyEffectContext effectContext) {
        List<Entity> targetEntities = new List<Entity>(5);
        switch (effectContext.Data.ApplyTarget) {
            case Effect.ApplyTarget.None:
                break;
            case Effect.ApplyTarget.Self:
                targetEntities.Add(effectContext.Caster);
                break;
            case Effect.ApplyTarget.ClosestEnemy:
                if (effectContext.Targets.Count == 0) { Debug.LogError($"<color=orange>가까운 타겟을 선택했으나 대상이 없음. 의도치 않은 동작</color>"); return null; }
                targetEntities.AddRange(effectContext.Targets);
                break;
            default:
                break;
        }

        return targetEntities;
    }
}
