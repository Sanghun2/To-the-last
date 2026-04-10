using UnityEngine;

public class DialogEncounterDataParser : EncounterDataParserBase<DialogEncounterSD, DialogEncounterData>
{
    public override DialogEncounterData ParseData(DialogEncounterSD encounterSD) {
        return new DialogEncounterData(
            encounterSD.ID,
            encounterSD.EventImage,
            encounterSD.Description,
            encounterSD.SelectionList,
            encounterSD.Dialog
            );
    }
}
