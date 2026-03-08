using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "StatModifierEffectSD", menuName = "Scriptable Objects/Effect/Stat/StatModifierEffectSD")]
public class ApplyStatModifierEffectSD : ModifyEffectSD
{
    [SerializeField] Define.Stat targetStat;

    public override void ApplyEffect(Entity caster, Entity target) {
        if (!IsValid(caster, target)) return;

        if (target is BattleEntity battleEntity) {
            if (!battleEntity.TryGetStat(targetStat.ToID(), out Stat stat)) { return; }

            var newModifier = new StatModifier(Value, ConvertType(operatorType));
            stat.AddModifier(newModifier);
        }
        else {
            Debug.LogError($"<color=red>stat modifier를 추가 할 수 없음</color>");
        }
    }

    private StatModifier.ModifierType ConvertType(Effect.OperatorType operatorType) {
        switch (operatorType) {
            case Effect.OperatorType.Add:
                return StatModifier.ModifierType.PureAdd;
            case Effect.OperatorType.Multiply:
                return StatModifier.ModifierType.PureMultiply;
            default:
                return StatModifier.ModifierType.None;
        }
    }
}
