using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitSD", menuName = "Scriptable Objects/TraitSD")]
public class TraitSD : ImageSDBase
{
    public int Cost => cost;

    [SerializeField] int cost;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_TraitSD");
    }
}
