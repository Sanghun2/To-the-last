using BilliotGames;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemSD", menuName = "Scriptable Objects/ItemSD")]
public class ItemSD : SDBase
{
    public Sprite ItemImage => itemImage;

    [SerializeField] Sprite itemImage;

    private void OnValidate() {
        RenameAsset(ID, suffix: "_ItemSD");
    }
}
