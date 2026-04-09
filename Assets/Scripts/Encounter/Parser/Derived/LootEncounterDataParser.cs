using UnityEngine;

public class LootEncounterDataParser : EncounterDataParserBase
{
    public override EncounterDataBase ParseData(EncounterSDBase encounterSD) {
        var lootEncounterSD = encounterSD as LootEncounterSD;
        if (lootEncounterSD != null) {
            var data = new LootEncounterData(
                lootEncounterSD.EventImage,
                lootEncounterSD.Description,
                lootEncounterSD.SelectionList);

            // sd -> _data 처리

            return data;
        }

        return null;
    }
}
