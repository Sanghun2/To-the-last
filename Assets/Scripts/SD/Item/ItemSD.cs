using BilliotGames;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemSD", menuName = "Scriptable Objects/ItemSD")]
public class ItemSD : ImageSDBase
{
    public Sprite ItemImage => Image;
    public int MaxStackCount => maxStackCount;

    [SerializeField] int maxStackCount;

    private void OnValidate() {
        RenameAsset(ID, suffix: "_ItemSD");
    }
}
