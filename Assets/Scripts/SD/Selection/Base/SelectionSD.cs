using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectionSD", menuName = "Scriptable Objects/SelectionSD")]
public abstract class SelectionSD : SDBase
{
    public Ingredient Requirement => requirement.ItemSD == null ? null : requirement;
    public Define.RequirementType RequirementType => requirementType;
    public int RequireMinutes => requireMinutes;

    [Space]
    [SerializeField] protected int requireMinutes;
    [SerializeField] protected Define.RequirementType requirementType;
    [SerializeField] protected Ingredient requirement;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:$"_{GetType()}");
    }
}
