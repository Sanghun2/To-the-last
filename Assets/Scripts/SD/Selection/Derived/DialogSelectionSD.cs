using UnityEngine;

[CreateAssetMenu(fileName = "TalkSelectionSD", menuName = "Scriptable Objects/Selection/DialogSelectionSD")]
public class DialogSelectionSD : SelectionSDBase
{
    private void OnValidate() {
        RenameAsset(ID, suffix: "_DialogSelectionSD");
    }
}
