using UnityEngine;

public abstract class ModifyEffectSD : EffectSD
{
    public Effect.ValueType ValueType => valueType;
    public Effect.OperatorType OperatorType => operatorType;


    [SerializeField] protected Effect.ValueType valueType;
    [SerializeField] protected Effect.OperatorType operatorType;

    protected (float resultValue, float deltaValue) CalculateAppyingValue(float targetValue) {
        float prevValue = targetValue;
        switch (operatorType) {
            case Effect.OperatorType.Add:
                //targetValue += Value;
                break;
            case Effect.OperatorType.Multiply:
                //targetValue *= Value;
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
