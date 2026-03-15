using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSD", menuName = "Scriptable Objects/CharacterSD")]
public class CharacterSD : SDBase
{
    public Sprite MainCharacterImage => mainCharacterImage;
    public Animator Animator => animator;

    [SerializeField] Sprite mainCharacterImage;
    [SerializeField] Animator animator;

    private void OnValidate() {
        RenameAsset(ID, suffix: $"_CharacterSD");
    }
}
