using UnityEngine;

[CreateAssetMenu(fileName = "StatModifyEffectSD", menuName = "Scriptable Objects/Effect/Stat/Stat Modify Effect SD")]
public class StatModifyEffectSD : ModifyEffectSD
{
    [SerializeField] Define.Stat targetStat;

    public override void ApplyEffect(Entity caster, Entity target) {
        if (!IsValid(caster, target)) return;

        if (target is BattleEntity targetEntity) {
            if (!targetEntity.TryGetStatValue(targetStat, out float currentValue)) { Debug.LogError($"<color=red>target이 stat({targetStat})울 가지고 있지 않음</color>"); return; }

            var result = CalculateValue(currentValue);
            targetEntity.TryChangeStat(targetStat.ToID(), result.deltaValue);
            var battleUI = Managers.UI.GetUI<BattleUI>();
            EntityUI targetEntityUI = battleUI.GetEntityUI(targetEntity);


            var context = new FloatingTextContext(
                result.deltaValue.ToString(),
                targetEntityUI.transform.position,
                FloatingText.TextType.Damage
                );

            battleUI.FloatingText.ShowText(context);

            // test
            targetEntity.TryGetStatValue(targetStat, out float viewValue);
            Debug.Log($"<color=yellow>({target.EntityID})의 ({targetStat.ToID()}) ({result.deltaValue}) 감소. 남은 체력:({viewValue})</color>");
            // ---
        }
        else {
            Debug.LogError($"<color=red>entity가 battle entity가 아니어서 stat을 변경할 수 없음</color>");
        }
    }

    protected override void OnValidate() {
        RenameAsset(ID, suffix:"_StatModifyEffectSD");
    }
}
