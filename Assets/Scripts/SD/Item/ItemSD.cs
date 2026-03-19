using BilliotGames;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemSD", menuName = "Scriptable Objects/ItemSD")]
public class ItemSD : ImageSDBase
{
    public Sprite ItemImage => Image;
    public int MaxStackCount => maxStackCount;

    [SerializeField] int maxStackCount;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: "_ItemSD");
    }
}
