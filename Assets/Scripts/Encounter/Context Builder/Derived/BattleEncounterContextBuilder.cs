using System;
using UnityEngine;

public class BattleEncounterContextBuilder : EncounterContextBuilderBase<BattleEncounterData, BattleEncounterContext>
{
    public override BattleEncounterContext BuildContext(BattleEncounterData data) {
        return new BattleEncounterContext(data);
    }
}
