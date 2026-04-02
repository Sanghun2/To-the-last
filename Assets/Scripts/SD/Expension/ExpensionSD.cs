using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "ExpensionSD", menuName = "Scriptable Objects/ExpensionSD")]
public class ExpensionSD : SDBase, IRequirementContent
{
    public IReadOnlyList<Ingredient> Requirements => requirements;
    public int ExpensionLevel => expensionLevel;

    [SerializeField] int expensionLevel;
    [SerializeField] Ingredient[] requirements;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_ExpensionSD");
    }
}
