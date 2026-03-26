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
    public IReadOnlyList<StatData> StatList => statList;

    [SerializeField] string[] features;
    [SerializeField] bool isDefaultCharacter;
    [SerializeField] MetabolismSD metabolismSD;
    [SerializeField] List<StatData> statList;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: $"_CharacterSD");
    }
}
