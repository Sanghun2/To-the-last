using System;
using UnityEngine;

public class SpecialEncounterContextBuilder : IEncounterContextBuilder
{
    public Type TargetSDType => typeof(SpecialEncounterContext);

    public BaseEncounterContext BuildContext(EncounterDataBase data) {
        var specialEncounterSD = (SpecialEncounterData)data;
        var context = new SpecialEncounterContext(specialEncounterSD);
        return context;
    }
}
