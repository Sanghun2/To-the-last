using BilliotGames;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemSD", menuName = "Scriptable Objects/ItemSD")]
public class ItemSD : ImageSDBase
{
    public int MaxStackCount => maxStackCount;
    public int Weight => weight;


    [SerializeField] int weight = 2;
    [SerializeField] int maxStackCount;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: "_ItemSD");
    }
}
