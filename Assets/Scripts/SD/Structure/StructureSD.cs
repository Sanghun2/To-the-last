using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public abstract class StructureSD : IconSDBase
{
    public IReadOnlyList<Ingredient> RequirementItems => requirementItems;
    public int ConstructionTime => constructionTime;
    public bool Locked => locked;

    [SerializeField] protected bool locked=true;
    [SerializeField] protected int constructionTime = 100;
    [SerializeField] protected Ingredient[] requirementItems;


    public void LockConstruction(bool @lock) {
        locked = @lock;
    }

    private void OnValidate() {
        RenameAsset(ID, suffix: "_StructureSD");
    }

    public abstract Type GetUIType();
}
