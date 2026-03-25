using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSD", menuName = "Scriptable Objects/StatSD")]
public class StatSD : ImageSDBase
{
    public Define.Stat TargetStat => targetStat;
    public Define.StatType StatType => statType;

    [SerializeField] Define.Stat targetStat;
    [SerializeField] Define.StatType statType;

    protected virtual void OnValidate() {
        if (id.Equals(targetStat)) return;

        id = targetStat.ToString();
        RenameAsset(targetStat.ToString(), suffix:"_StatSD");
    }
}
