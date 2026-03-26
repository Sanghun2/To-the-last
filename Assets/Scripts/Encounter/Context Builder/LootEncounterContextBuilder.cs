using System;
using UnityEngine;

public class LootEncounterContextBuilder : EncounterContextBuilderBase<LootEncounterData, LootEncounterContext>
{
    public override LootEncounterContext BuildContext(LootEncounterData data) {
        var context = new LootEncounterContext(data);
        return context;
    }
}
