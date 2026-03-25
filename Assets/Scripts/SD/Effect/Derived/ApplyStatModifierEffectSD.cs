using BilliotGames;
using UnityEngine;

public enum ApplyType
{
    None,
    Add,
    Remove,
}

[CreateAssetMenu(fileName = "ApplyStatModifierEffectSD", menuName = "Scriptable Objects/Effect/Stat/Apply Stat Modifier Effect SD")]
public class ApplyStatModifierEffectSD : ModifyEffectSD
{
    [SerializeField] ApplyType applyType;
    [SerializeField] Define.Stat targetStat;

    public override void ApplyEffect(Entity caster, Entity target) {
        if (!IsValid(caster, target)) return;

        if (target is BattleEntity battleEntity) {
            if (!battleEntity.TryGetStat(targetStat.ToID(), out IStatEntry stat)) { return; }

            var newModifier = new StatModifier(ID, Value, ConvertType(operatorType));
            switch (applyType) {
                case ApplyType.Add:
                    //stat.AddModifier(newModifier);
                    break;
                case ApplyType.Remove:
                    //stat.RemoveModifier(newModifier);
                    break;
                case ApplyType.None:
                default:
                    break;
            }
            Debug.Log("stat modifier event 기능 구현 필요");
        }
        else {
            Debug.LogError($"<color=red>stat modifier를 추가 할 수 없음</color>");
        }
    }

    protected override void OnValidate() {
        RenameAsset(ID, suffix: "_ApplyStatModifierEffectSD");
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
