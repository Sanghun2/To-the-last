using System.Collections.Generic;
using UnityEngine;

public class LootEncounterData : EncounterDataBase
{
    public LootEncounterData(
        Sprite eventImage, 
        string description, 
        IReadOnlyList<SelectionSDContext> selectList) : base(eventImage, description, selectList) {

    }
}
