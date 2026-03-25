using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "MetabolismSD", menuName = "Scriptable Objects/MetabolismSD")]
public class MetabolismSD : SDBase
{
    public IReadOnlyList<MetabolismData> ConsumeInfos => consumeInfos;

    [SerializeField] MetabolismData[] consumeInfos;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: "_MetabolismSD");
    }
}

[Serializable]
public class MetabolismData
{
    public Define.Stat TargetStat => targetStat;
    public float ConsumeAmount => consumeAmount;

    [SerializeField] Define.Stat targetStat;
    [SerializeField] float consumeAmount;
}
