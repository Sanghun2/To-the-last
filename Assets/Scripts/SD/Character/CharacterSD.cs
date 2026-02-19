using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSD", menuName = "Scriptable Objects/CharacterSD")]
public class CharacterSD : IconSDBase
{
    private void OnValidate() {
        RenameAsset(ID, suffix: $"_CharacterSD");
    }
}
