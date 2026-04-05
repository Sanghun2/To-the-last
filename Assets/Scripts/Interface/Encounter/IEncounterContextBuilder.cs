using System;
using UnityEngine;

public interface IEncounterContextBuilder
{
    public EncounterContextBase BuildContext(EncounterDataBase data);
}

