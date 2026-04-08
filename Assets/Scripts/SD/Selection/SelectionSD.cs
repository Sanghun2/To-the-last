using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectionSD", menuName = "Scriptable Objects/Selection/SelectionSD")]
public class SelectionSD : SDBase
{
    public IReadOnlyList<Condition> UnlockConditions => unlockConditions;
    public Condition ConditionToSelect => conditionToSelect;
    public int RequireMinutes => requireMinutes;


    [Space]
    [SerializeField] Condition[] unlockConditions;
    [SerializeField] Condition conditionToSelect;
    [SerializeField] protected int requireMinutes;

    protected override void OnValidate() {
        RenameAsset(ID, suffix:$"_{GetType()}");
    }
}
