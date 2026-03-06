using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectSD", menuName = "Scriptable Objects/EffectSD")]
public class EffectSD : SDBase
{
    public enum OperatorType {
        Add,
        Multiply,
    }
    public enum ValueType {
        Scala,
        Percent,
    }
    public enum TargetType {
        None,

        // 기본 stat
        Hp,
        Hunger,
        Thirst,
        Mental,
        Temperture,

        // 확장 stat
        Strength,
        Agility,
        Focus,

        // 전투
        Attack, // 무기의 공격력
        Defense,

        Damage,  // 데미지 적용을 위해 적용되는 데미지
        Dodge, 
        Charge,
    }
    public enum ApplyType {
        Instant,
        Delay,
    }

    public TargetType TargetType_ => targetTyep;
    public ValueType ValueType_ => valueType;
    public OperatorType OperatorType_ => operatorType;
    public ApplyType ApplyType_ => applyType;
    public float Value => valueType == ValueType.Scala ? value : value * 0.01f;

    [SerializeField] TargetType targetTyep;
    [SerializeField] protected float value;
    [SerializeField] protected OperatorType operatorType;
    [SerializeField] protected ValueType valueType;
    [SerializeField] protected ApplyType applyType;

    private void OnValidate() {
        RenameAsset(ID, suffix:"_EffectSD");
    }
}
