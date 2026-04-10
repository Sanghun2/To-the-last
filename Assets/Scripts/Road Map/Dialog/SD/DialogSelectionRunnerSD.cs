using UnityEngine;

[CreateAssetMenu(fileName = "DialogSelectionRunnerSD", menuName = "Scriptable Objects/Selection/Runner/DialogSelectionRunnerSD")]
public class DialogSelectionRunnerSD : SelectionRunnerSDBase
{
    public DialogBookSD Dialog => dialog;

    [SerializeField] DialogBookSD dialog;
}
