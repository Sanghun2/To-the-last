using UnityEngine;

public class DialogSelectionRunnerContext : SelectionRunnerContextBase
{
    public string DialogID { get; }

    public DialogSelectionRunnerContext(string dialogID, int jobDuration) : base(jobDuration) {
        DialogID = dialogID;
    }
}
