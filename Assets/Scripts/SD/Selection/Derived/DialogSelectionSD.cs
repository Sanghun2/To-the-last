using UnityEngine;

[CreateAssetMenu(fileName = "TalkSelectionSD", menuName = "Scriptable Objects/Selection/TalkSelectionSD")]
public class DialogSelectionSD : SelectionSD
{
    private void OnValidate() {
        RenameAsset(ID, suffix: "_DialogSelectionSD");
    }
}
