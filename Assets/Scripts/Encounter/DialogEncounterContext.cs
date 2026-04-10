using UnityEngine;

public class DialogEncounterContext : EncounterContextBase<DialogEncounterData>
{
    public DialogBookData BookData { get; }

    public DialogEncounterContext(DialogEncounterData encounterData) : base(encounterData) {
        BookData = encounterData.BookData;
    }
}
