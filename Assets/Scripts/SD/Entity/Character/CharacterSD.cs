using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSD", menuName = "Scriptable Objects/CharacterSD")]
public class CharacterSD : EntitySDBase
{
    public RuntimeAnimatorController Animator => animator;

    [SerializeField] RuntimeAnimatorController animator;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: $"_CharacterSD");
    }
}
