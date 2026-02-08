using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StructureSD", menuName = "Scriptable Objects/StructureSD")]
public class StructureSD : IconSDBase
{
    public IReadOnlyList<Ingredient> RequirementItems => requirementItems;

    public int ConstructionTime => constructionTime;

    [SerializeField] int constructionTime = 100;
    [SerializeField] Ingredient[] requirementItems;


    private void OnValidate() {
        RenameAsset(ID, suffix: "_StructureSD");
    }
}
