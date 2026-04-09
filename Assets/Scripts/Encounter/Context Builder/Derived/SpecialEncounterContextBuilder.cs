using System;
using UnityEngine;

public class SpecialEncounterContextBuilder : IEncounterContextBuilder
{
    public Type TargetSDType => typeof(SpecialEncounterContext);

    public EncounterContextBase BuildEncounterContext(EncounterDataBase data) {
        var specialEncounterSD = (SpecialEncounterData)data;
        var context = new SpecialEncounterContext(specialEncounterSD);
        return context;
    }
}
