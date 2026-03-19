using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

[Icon("Assets/Layer Lab/GUI Pro-SurvivalClean/ResourcesData/Sprites/Components/Icon_PictoIcons(x2)/64/Icon_Skull.Png")]
[CreateAssetMenu(fileName = "EnemySD", menuName = "Scriptable Objects/EnemySD")]
public class EnemySD : EntitySDBase
{
    public IReadOnlyList<StatData> StatDataList => statDataList;

    [SerializeField]
    [ContextMenuItem("[  Reset Stats  ]", nameof(ResetStats))]
    List<StatData> statDataList = new List<StatData>() {
        new StatData(Define.Stat.Hp, StatData.StatType.BoundedStat, 100),
        new StatData(Define.Stat.Strength, StatData.StatType.Stat, 20),
        new StatData(Define.Stat.Agility, StatData.StatType.Stat, 10),
        new StatData(Define.Stat.Focus, StatData.StatType.Stat,10),
        new StatData(Define.Stat.Toughness, StatData.StatType.Stat,20),
    };

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix: "_EnemySD");
    }

    private void ResetStats() {
        statDataList = new List<StatData>() {
        new StatData(Define.Stat.Hp, StatData.StatType.BoundedStat, 100),
        new StatData(Define.Stat.Strength, StatData.StatType.Stat, 20),
        new StatData(Define.Stat.Agility, StatData.StatType.Stat, 10),
        new StatData(Define.Stat.Focus, StatData.StatType.Stat, 10),
        new StatData(Define.Stat.Toughness, StatData.StatType.Stat, 20),
    };
    }
}

[Serializable]
public class StatData
{
    public enum StatType
    {
        Stat,
        BoundedStat,
    }

    public StatType Type => type;
    public Define.Stat Stat => stat;
    public float Value => value;

    [SerializeField] StatType type;
    [SerializeField] Define.Stat stat;
    [SerializeField] float value;

    public StatData(Define.Stat stat, StatType type, float value) {
        this.stat = stat;
        this.type = type;
        this.value = value;
    }
}
