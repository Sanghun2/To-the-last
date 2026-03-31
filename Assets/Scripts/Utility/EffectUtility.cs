using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectUtility
{
    public static IReadOnlyList<Entity> GetTargets(BattleEntity caster, Effect.ApplyTarget targetType) {
        switch (targetType) {
            case Effect.ApplyTarget.None:
            case Effect.ApplyTarget.Self:
                return null;
            case Effect.ApplyTarget.ClosestEnemy:
                Debug.Log($"<color=cyan>({ImplementRequiredMessage(targetType)})</color>");
                return null;
            default:
                Debug.LogError($"<color=red>no target type of ({targetType}) is exist</color>");
                return null;
        }
    }

    private static object ImplementRequiredMessage(Effect.ApplyTarget targetType) {
        return $"get target ({targetType}) implement required";
    }
}
