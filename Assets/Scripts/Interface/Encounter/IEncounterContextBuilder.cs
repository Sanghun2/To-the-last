using System;
using UnityEngine;

public interface IEncounterContextBuilder
{
    public EncounterContextBase BuildEncounterContext(EncounterDataBase data);
}

