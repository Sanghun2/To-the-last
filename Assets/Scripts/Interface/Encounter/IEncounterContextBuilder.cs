using System;
using UnityEngine;

public interface IEncounterContextBuilder
{
    public BaseEncounterContext BuildContext(EncounterDataBase data);
}

