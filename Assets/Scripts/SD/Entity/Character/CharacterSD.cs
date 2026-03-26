using System;
using System.Collections.Generic;
using System.Text;
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
        CheckEssentialStats();
    }

    private void CheckEssentialStats() {
        HashSet<Define.Stat> essentialStats = new HashSet<Define.Stat>() {
            Define.Stat.Hp,
            Define.Stat.Hunger,
            Define.Stat.Thirst,
            Define.Stat.Mental,
            Define.Stat.Temperature,
            Define.Stat.Strength,
            Define.Stat.Agility,
            Define.Stat.Toughness,
            Define.Stat.Focus,
        };

        int essentialCount = essentialStats.Count;
        int count = 0;
        for (int i = 0; i < statList.Count; i++) {
            var stat = statList[i];
            if (essentialStats.Contains(stat.Stat)) {
                essentialStats.Remove(stat.Stat);
                count++;
            }
        }

        if (count != essentialCount) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<color=red>({ID})의 필수 stat이 없음</color>");
            foreach (var remainStat in essentialStats) {
                sb.AppendLine($"{remainStat}");
            }

            Debug.LogError(sb.ToString());
        }
    }
}
