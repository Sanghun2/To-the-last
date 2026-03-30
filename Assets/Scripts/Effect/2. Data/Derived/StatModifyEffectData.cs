using UnityEngine;

public class StatModifyEffectData : EffectDataBase
{
    public Effect.ApplyTarget ApplyTarget => applyTarget;
    public Define.Stat TargetStat => targetStat;

    private Effect.ApplyTarget applyTarget;
    private Define.Stat targetStat;

    public StatModifyEffectData(Effect.ApplyTarget applyTarget, Define.Stat targetStat) {
        this.applyTarget = applyTarget;
        this.targetStat = targetStat;
    }
}
