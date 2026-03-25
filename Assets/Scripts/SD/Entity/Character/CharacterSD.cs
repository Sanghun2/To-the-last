using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSD", menuName = "Scriptable Objects/CharacterSD")]
public class CharacterSD : EntitySDBase
{
    public string[] Features => features;
    public bool IsDefaultCharacter => isDefaultCharacter;
    public IReadOnlyList<MetabolismData> MetabolismDatas => metabolismSD.ConsumeInfos;

    [SerializeField] string[] features;
    [SerializeField] bool isDefaultCharacter;
    [SerializeField] MetabolismSD metabolismSD;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: $"_CharacterSD");
    }
}
