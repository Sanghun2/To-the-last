using UnityEngine;

public class StatModifyEffectDataParser : EffectDataParserBase<StatModifyEffectSD, StatModifyEffectData>
{
    public override bool TryParse(StatModifyEffectSD effectSD, out StatModifyEffectData effectData) {
        effectData = new StatModifyEffectData();
        return true;
    }
}
