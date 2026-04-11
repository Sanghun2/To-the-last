using UnityEngine;

public class DialogSelectionRunnerData : SelectionRunnerDataBase
{
    public string DialogID { get; }

    public DialogSelectionRunnerData(string dialogID) {
        DialogID = dialogID;
    }
}
