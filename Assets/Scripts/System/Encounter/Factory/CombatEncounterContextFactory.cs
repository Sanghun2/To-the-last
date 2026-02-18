using System;
using UnityEngine;

public class CombatEncounterContextFactory : IEncounterContextFactory
{
    public Type TargetSDType => typeof(CombatEncounterContext);

    public EncounterContext CreateContext(EncounterSD sd) {
        var combatEncounterSD = (CombatEncounterSD)sd;
        var context = new CombatEncounterContext(combatEncounterSD);        
        return context;
    }
}
