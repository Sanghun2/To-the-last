using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSD", menuName = "Scriptable Objects/Recipe/RecipeSD")]
public class RecipeSD : ContentSDBase
{
    public override Sprite Image => outputs.Length > 0 ? outputs[0].ItemSD.Image : null;
    public IReadOnlyList<Ingredient> Outputs => outputs;

    [SerializeField] protected Ingredient[] outputs;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: "_RecipeSD");
    }
}

[Serializable]
public class Ingredient
{
    public ItemSD ItemSD => itemSD;
    public int Amount => amount;

    [SerializeField] ItemSD itemSD;
    [SerializeField] int amount;

    public Ingredient(ItemSD itemSD, int amount) {
        this.itemSD = itemSD;
        this.amount = amount;
    }
}

