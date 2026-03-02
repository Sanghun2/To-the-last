using UnityEngine;

[CreateAssetMenu(fileName = "BattleSelectionSD", menuName = "Scriptable Objects/Selection/BattleSelectionSD")]
public class BattleSelectionSD : SelectionSD
{
    public EnemySD[] Enemies => enemies;

    [SerializeField] EnemySD[] enemies;

    private void OnValidate() {
        RenameAsset(ID, suffix:"_BattleSelectionSD");
    }
}
