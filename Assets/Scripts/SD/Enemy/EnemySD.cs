using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemySD", menuName = "Scriptable Objects/EnemySD")]
public class EnemySD : SDBase
{
    public Sprite EnemyImage => enemyImage;
    public float Hp => hp;

    [SerializeField] Sprite enemyImage;
    [SerializeField] float hp;

    private void OnValidate() {
        RenameAsset(ID, suffix:"_EnemySD");
    }
}
