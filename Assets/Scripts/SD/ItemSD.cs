using BilliotGames;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemSD", menuName = "Scriptable Objects/ItemSD")]
public class ItemSD : IconSDBase
{
    public Sprite ItemImage => IconImage;

    private void OnValidate() {
        RenameAsset(ID, suffix: "_ItemSD");
    }
}
