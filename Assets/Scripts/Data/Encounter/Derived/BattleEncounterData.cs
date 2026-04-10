using System.Collections.Generic;
using UnityEngine;

public class BattleEncounterData : EncounterDataBase
{
    public BattleEncounterData(string id, Sprite eventImage, string description, IReadOnlyList<SelectionSDContext> selectList) 
        : base(id, eventImage, description, selectList) {
    }
}
