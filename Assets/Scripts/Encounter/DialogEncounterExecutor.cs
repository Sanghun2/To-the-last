using UnityEngine;

public class DialogEncounterExecutor : EncounterExecutorBase<DialogEncounterData, DialogEncounterContext>
{
    public override void ExecuteEncounter(DialogEncounterContext encounterContext) {
        Debug.Log($"try dialog executed");
        if (Managers.Dialog.TryStartDialog(encounterContext.BookData.ID, out var startedDialog)) {
            
        }
    }
}
