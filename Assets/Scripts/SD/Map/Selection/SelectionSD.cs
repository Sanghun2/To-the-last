using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectionSD", menuName = "Scriptable Objects/SelectionSD")]
public abstract class SelectionSD : SDBase
{
    public Ingredient Requirement => string.IsNullOrEmpty(requirement.ItemSD.ID) ? null : requirement;
    public Define.RequirementType RequirementType => requirementType;

    [Space]
    [SerializeField] protected Define.RequirementType requirementType;
    [SerializeField] protected Ingredient requirement;

    private void OnValidate() {
        RenameAsset(ID, suffix:$"_{GetType()}");
    }
}
