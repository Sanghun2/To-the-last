using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSD", menuName = "Scriptable Objects/Recipe/RecipeSD")]
public class RecipeSD : TimeBasedSD
{
    public IReadOnlyList<Ingredient> Inputs => inputs;
    public IReadOnlyList<Ingredient> Outputs => outputs;

    [SerializeField] Ingredient[] inputs;
    [SerializeField] Ingredient[] outputs;

    private void OnValidate() {
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
}

