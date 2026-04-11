using BilliotGames;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemSD", menuName = "Scriptable Objects/ItemSD")]
public class ItemSD : ImageSDBase
{
    public int MaxStackCount => maxStackCount;
    public int Weight => weight;
    public float Value => value;

    [SerializeField] int weight = 2;
    [SerializeField] float value = 1;
    [SerializeField] int maxStackCount;

    protected override void OnValidate() {
        RenameAsset(ID, suffix: "_ItemSD");
    }
}
