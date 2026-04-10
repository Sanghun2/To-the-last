using System.Collections.Generic;
using UnityEngine;

public class LootEncounterData : EncounterDataBase
{
    public LootEncounterData(
        string id,
        Sprite eventImage, 
        string description, 
        IReadOnlyList<SelectionSDContext> selectList) 
        : base(id, eventImage, description, selectList) {

    }
}
