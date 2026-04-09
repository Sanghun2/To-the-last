using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionContentSD", menuName = "Scriptable Objects/Content/ProductionContentSD")]
public class ProductionContentSD : ContentSDBase
{
    public override Sprite Image => outputs.Length > 0 ? outputs[0].ItemSD.Image : null;
    public IReadOnlyList<Ingredient> Outputs => outputs;

    [SerializeField] protected Ingredient[] outputs;

    protected override void OnValidate() {
        RenameAsset(ID, suffix: "_ProductionContentSD");
    }
}

[Serializable]
public class Ingredient : Requirement
{
    public ItemSD ItemSD => itemSD;

    [SerializeField] ItemSD itemSD;

    public Ingredient(ItemSD itemSD, int amount) : base(itemSD.Image, amount){
        this.itemSD = itemSD;
    }
}

public class Requirement
{
    public Sprite Image => image;
    public int Amount => amount;

    private Sprite image;
    private int amount;

    public Requirement(Sprite image, int amount) {
        this.image = image;
        this.amount = amount;
    }
}

