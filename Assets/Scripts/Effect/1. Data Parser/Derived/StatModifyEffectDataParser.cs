using UnityEngine;

public class StatModifyEffectDataParser : EffectDataParserBase<StatModifyEffectSD, StatModifyEffectData>
{
    public override bool TryParse(StatModifyEffectSD effectSD, float value, out StatModifyEffectData effectData) {
        effectData = new StatModifyEffectData(
            effectSD.TargetType, 
            effectSD.TargetStat);
        return true;
    }
}
