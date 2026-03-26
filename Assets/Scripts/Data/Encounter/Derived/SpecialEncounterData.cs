using System.Collections.Generic;
using UnityEngine;

public class SpecialEncounterData : EncounterDataBase
{
    public SpecialEncounterData(Sprite eventImage, string description, IReadOnlyList<SelectionSD> selectList) : base(eventImage, description, selectList) {
    }
}
