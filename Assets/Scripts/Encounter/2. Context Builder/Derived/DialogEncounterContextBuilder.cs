using UnityEngine;

public class DialogEncounterContextBuilder : EncounterContextBuilderBase<DialogEncounterData, DialogEncounterContext>
{
    public override DialogEncounterContext BuildContext(DialogEncounterData data) {
        return new DialogEncounterContext(data);
    }
}
