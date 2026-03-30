using System;
using UnityEngine;

public abstract class EffectDataParserBase
{
    public abstract bool TryParse(Effect effect, out EffectDataBase effectData);
}

public abstract class EffectDataParserBase<TSD, TData> : EffectDataParserBase
    where TSD : EffectSD
    where TData : EffectDataBase
{
    public override bool TryParse(Effect effect, out EffectDataBase effectData) {
        if (effect.EffectSD is TSD tsd) {
            var result = TryParse(tsd, effect.Value, out TData data);
            data.SetValue(effect.Value);
            effectData = data;
            return result;
        }

        Debug.LogError($"<color=red>({effect.GetType()}) is not type of ({typeof(TSD)})</color>");
        effectData = null;
        return false;
    }

    public abstract bool TryParse(TSD effectSD, float value, out TData effectData);
}
