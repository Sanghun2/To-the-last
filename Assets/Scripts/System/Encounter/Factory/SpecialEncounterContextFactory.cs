using System;
using UnityEngine;

public class SpecialEncounterContextFactory : IEncounterContextFactory
{
    public Type TargetSDType => typeof(SpecialEncounterContext);

    public EncounterContext CreateContext(EncounterSD sd) {
        var specialEncounterSD = (SpecialEncounterSD)sd;
        var context = new SpecialEncounterContext(specialEncounterSD);
        return context;
    }
}
