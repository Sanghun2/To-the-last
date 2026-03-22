using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSD", menuName = "Scriptable Objects/CharacterSD")]
public class CharacterSD : EntitySDBase
{
    public RuntimeAnimatorController Animator => animator;

    public string[] Features => features;

    [SerializeField] RuntimeAnimatorController animator;
    [SerializeField] string[] features;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: $"_CharacterSD");
    }
}
