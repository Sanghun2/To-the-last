using System.Collections.Generic;
using UnityEngine;

public class BattleEncounterData : EncounterDataBase
{
    public BattleEncounterData(Sprite eventImage, string description, IReadOnlyList<SelectionPair> selectList) : base(eventImage, description, selectList) {
    }
}
