using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSD", menuName = "Scriptable Objects/StatSD")]
public class StatSD : SDBase
{
    public Define.Stat StatType => statType;

    [SerializeField] Define.Stat statType;

    private void OnValidate() {
        if (id.Equals(statType)) return;

        id = statType.ToString();
        RenameAsset(statType.ToString(), suffix:"_StatSD");
    }
}
