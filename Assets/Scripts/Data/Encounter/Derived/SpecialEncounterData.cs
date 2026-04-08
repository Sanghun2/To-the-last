using System.Collections.Generic;
using UnityEngine;

public class SpecialEncounterData : EncounterDataBase
{
    public SpecialEncounterData(Sprite eventImage, string description, IReadOnlyList<SelectionPair> selectList) : base(eventImage, description, selectList) {
    }
}
