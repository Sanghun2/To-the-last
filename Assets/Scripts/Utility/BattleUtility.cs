using UnityEngine;

public static class BattleUtility
{
    private const float STRENGTH_VALUE = 1f;
    private const float AGILITY_VALUE = 2f;

    public static float CalculateBehaviourSpeed(BattleEntity entity) {
        if (!entity.TryGetStatValue(Define.Stat.Strength, out var strength)) {
            Debug.LogError($"<color=red>{Define.Stat.Strength} stat 없음</color>");
        }

        if (!entity.TryGetStatValue(Define.Stat.Agility, out var agility)) {
            Debug.LogError($"<color=red>{Define.Stat.Agility} stat 없음</color>");
        }

        return strength * STRENGTH_VALUE + agility * AGILITY_VALUE;
    }
}
