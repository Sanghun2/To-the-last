using UnityEngine;

public class Calculator
{
    public static (float newValue, float deltaValue) CalculateValue(float baseValue, float modifyingValue, Effect.OperatorType operatorType) {
        float newValue = baseValue;
        switch (operatorType) {
            case Effect.OperatorType.Add:
                newValue += modifyingValue;
                break;
            case Effect.OperatorType.Multiply:
                newValue*= modifyingValue;
                break;
            default:
                break;
        }

        return (newValue, newValue - baseValue);
    }
}
