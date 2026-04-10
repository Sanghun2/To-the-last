using UnityEngine;

public class BattleEncounterDataParser : EncounterDataParserBase<BattleEncounterSD, BattleEncounterData>
{
    public override BattleEncounterData ParseData(BattleEncounterSD encounterSD) {
        return new BattleEncounterData(
            encounterSD.ID,
            encounterSD.EventImage,
            encounterSD.Description,
            encounterSD.SelectionList
            );
    }
}
