using UnityEngine;

public class BattleEncounterDataParser : EncounterDataParserBase
{
    public override EncounterDataBase ParseData(EncounterSDBase encounterSD) {
        return new BattleEncounterData(
            encounterSD.EventImage,
            encounterSD.Description,
            encounterSD.SelectionList
            );
    }
}
