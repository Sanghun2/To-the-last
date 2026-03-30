using BilliotGames;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StatModifyEffectContextProcessor : EffectContextProcessorBase<StatModifyEffectContext>
{
    public override void ApplyEffect(StatModifyEffectContext effectContext) {
        Entity target = ResolveTarget(effectContext);
        if (target != null) {
            //target
            StatContainer stats = null;
            if (stats.TryGetStat(effectContext.TargetStat.ToID(), out IStatEntry stat)) {
                stat.ChangeRawValue(effectContext.Value);
                Debug.Log($"stat changed. current?{stat.ModifiedValue}");
            }
        }
        else {
            Debug.Log("target is null");
        }
    }

    private Entity ResolveTarget(StatModifyEffectContext effectContext) {
        Entity targetEntity = null;
        switch (effectContext.Data.ApplyTarget) {
            case Effect.ApplyTarget.None:
                break;
            case Effect.ApplyTarget.Self:
                targetEntity = effectContext.Caster;
                break;
            case Effect.ApplyTarget.ClosestEnemy:
                if (effectContext.Targets.Count == 0) { Debug.LogError($"<color=orange>가까운 타겟을 선택했으나 대상이 없음. 의도치 않은 동작</color>"); return null; }
                targetEntity = effectContext.Targets[0];
                break;
            default:
                break;
        }

        return targetEntity;
    }
}
