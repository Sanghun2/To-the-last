using UnityEngine;

public class StatModifyEffectContextBuilder : EffectContextBuilderBase<StatModifyEffectData, StatModifyEffectContext>
{
    public override bool TryBuildContext(StatModifyEffectData effectData, out StatModifyEffectContext effectContext) {
        effectContext = new StatModifyEffectContext(effectData);
        //context.SetApplyContext();
        return true;
    }
}
