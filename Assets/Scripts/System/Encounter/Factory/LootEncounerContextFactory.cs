using System;
using UnityEngine;

public class LootEncounerContextFactory : IEncounterContextFactory
{
    public Type TargetSDType => typeof(LootEncounterSD);

    public EncounterContext CreateContext(EncounterSD sd) {
        var looEncounterSD = (LootEncounterSD)sd;
        return new LootEncounterContext(looEncounterSD);
    }
}
