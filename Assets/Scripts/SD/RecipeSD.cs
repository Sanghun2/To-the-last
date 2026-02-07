using System;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSD", menuName = "Scriptable Objects/RecipeSD")]
public class RecipeSD : IconSDBase
{
    public int RequireMinutes => requireMinutes;

    [SerializeField] int requireMinutes;
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

