using UnityEngine;

public class LootEncounterParser : EncounterParserBase
{
    public override bool TryParse(EncounterSD encounterSD, out EncounterDataBase data) {
        var lootEncounterSD = encounterSD as LootEncounterSD;
        if (lootEncounterSD != null) {
            data = new LootEncounterData(
                lootEncounterSD.EventImage,
                lootEncounterSD.Description,
                lootEncounterSD.SelectionList);

            // sd -> _data 처리

            return true;
        }

        data = null;
        return false;
    }
}
