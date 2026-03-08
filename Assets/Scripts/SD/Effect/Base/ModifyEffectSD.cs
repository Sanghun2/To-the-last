using UnityEngine;

public abstract class ModifyEffectSD : EffectSD
{
    public Effect.ValueType ValueType_ => valueType;
    public Effect.OperatorType OperatorType_ => operatorType;
    public float Value => valueType == Effect.ValueType.Scala ? _value : _value * 0.01f;


    [SerializeField] protected Effect.ValueType valueType;
    [SerializeField] protected float _value;
    [SerializeField] protected Effect.OperatorType operatorType;

    protected (float resultValue, float deltaValue) CalculateValue(float targetValue) {
        float prevValue = targetValue;
        switch (operatorType) {
            case Effect.OperatorType.Add:
                targetValue += Value;
                break;
            case Effect.OperatorType.Multiply:
                targetValue *= Value;
                break;
            default:
                break;
        }

        return (targetValue, targetValue - prevValue);
    }

    protected override void OnValidate() {
        RenameAsset(ID, suffix:"_ModifyEffectSD");
    }
}
