using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public abstract class StructureSD : IconSDBase
{
    public IReadOnlyList<Ingredient> RequirementItems => requirementItems;
    public int ConstructionTime => constructionTime;

    [SerializeField] protected int constructionTime = 100;
    [SerializeField] protected Ingredient[] requirementItems;


    private void OnValidate() {
        RenameAsset(ID, suffix: "_StructureSD");
    }
}
