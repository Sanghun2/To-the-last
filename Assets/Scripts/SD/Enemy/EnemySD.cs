using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

[Icon("Assets/Layer Lab/GUI Pro-SurvivalClean/ResourcesData/Sprites/Components/Icon_PictoIcons(x2)/64/Icon_Skull.Png")]
[CreateAssetMenu(fileName = "EnemySD", menuName = "Scriptable Objects/EnemySD")]
public class EnemySD : SDBase
{
    public Sprite EnemyImage => enemyImage;
    public IReadOnlyList<StatData> StatDataList => statDataList;

    [SerializeField] Sprite enemyImage;
    [SerializeField]
    [ContextMenuItem("[  Reset Stats  ]", nameof(ResetStats))]
    List<StatData> statDataList = new List<StatData>() {
        new StatData(Define.Stat.Hp, 100),
        new StatData(Define.Stat.Strength, 20),
        new StatData(Define.Stat.Agility, 10),
        new StatData(Define.Stat.Focus, 10),
        new StatData(Define.Stat.Toughness, 20),
    };

    private void OnValidate() {
        RenameAsset(ID, suffix:"_EnemySD");
    }

    private void ResetStats() {
        statDataList = new List<StatData>() {
        new StatData(Define.Stat.Hp, 100),
        new StatData(Define.Stat.Strength, 20),
        new StatData(Define.Stat.Agility, 10),
        new StatData(Define.Stat.Focus, 10),
        new StatData(Define.Stat.Toughness, 20),
    };
    }
}

[Serializable]
public class StatData
{
    public Define.Stat Stat => stat;
    public float Value => value;

    [SerializeField] Define.Stat stat;
    [SerializeField] float value;

    public StatData(Define.Stat stat, float value) {
        this.stat = stat;
        this.value = value;
    }
}
