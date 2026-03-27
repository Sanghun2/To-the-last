using System;
using UnityEngine;

public abstract class EffectDataParserBase
{
    public abstract bool TryParse(EffectSD effectSD, out EffectDataBase effectData);
}

public abstract class EffectDataParserBase<TSD, TData> : EffectDataParserBase
    where TSD : EffectSD
    where TData : EffectDataBase
{
    public override bool TryParse(EffectSD effectSD, out EffectDataBase effectData) {
        if (effectSD is TSD tsd) {
            var result = TryParse(tsd, out TData data);
            effectData = data;
            return result;
        }

        Debug.LogError($"<color=red>({effectSD.GetType()}) is not type of ({typeof(TSD)})</color>");
        effectData = null;
        return false;
    }

    public abstract bool TryParse(TSD effectSD, out TData effectData);
}
