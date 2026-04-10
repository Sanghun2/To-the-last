using UnityEngine;

public class LootEncounterDataParser : EncounterDataParserBase<LootEncounterSD, LootEncounterData>
{
    public override LootEncounterData ParseData(LootEncounterSD encounterSD) {
        var data = new LootEncounterData(
                encounterSD.ID,
                encounterSD.EventImage,
                encounterSD.Description,
                encounterSD.SelectionList);

        // sd -> _data 처리

        return data;
    }
}
