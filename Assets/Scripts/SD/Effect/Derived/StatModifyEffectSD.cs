using UnityEngine;

[CreateAssetMenu(fileName = "StatModifyEffectSD", menuName = "Scriptable Objects/Effect/Stat/Stat Modify Effect SD")]
public class StatModifyEffectSD : ModifyEffectSD
{
    [SerializeField] Define.Stat targetStat;

    public override void ApplyEffect(Entity caster, Entity target) {
        if (!IsValid(caster, target)) return;

        if (target is BattleEntity battleEntity) {
            if (!battleEntity.TryGetStatValue(targetStat, out float currentValue)) { return; }

            var result = CalculateValue(currentValue);
            battleEntity.TryChangeStat(targetStat.ToID(), result.deltaValue);
        }
        else {
            Debug.LogError($"<color=red>entity가 battle entity가 아니어서 stat을 변경할 수 없음</color>");
        }
    }

    protected override void OnValidate() {
        RenameAsset(ID, suffix:"_StatModifyEffectSD");
    }
}
