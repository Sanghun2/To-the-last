using System;
using UnityEngine;

public class BattleEncounterContextFactory : IEncounterContextFactory
{
    public Type TargetSDType => typeof(BattleEncounterContext);

    public EncounterContext CreateContext(EncounterSD sd) {
        var combatEncounterSD = (BattleEncounterSD)sd;
        var context = new BattleEncounterContext(combatEncounterSD);        
        return context;
    }
}
