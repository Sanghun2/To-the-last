using System;
using BilliotGames;
using UnityEngine;

public class EffectDataParserContainer : TypeRegistry<EffectSD, EffectDataParserBase>
{
    public EffectDataParserContainer() {
        Register<StatModifyEffectSD>(new StatModifyEffectDataParser());
    }

    public bool TryParseData(EffectSD effectSD, out object effectData) {
        throw new NotImplementedException();
    }
}
