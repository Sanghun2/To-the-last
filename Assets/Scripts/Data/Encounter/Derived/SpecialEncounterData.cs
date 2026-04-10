using System.Collections.Generic;
using UnityEngine;

public class SpecialEncounterData : EncounterDataBase
{
    public SpecialEncounterData(string id, Sprite eventImage, string description, IReadOnlyList<SelectionSDContext> selectList) 
        : base(id, eventImage, description, selectList) {
    }
}
