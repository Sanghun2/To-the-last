using BilliotGames;
using UnityEngine;

public static class StatUtility
{
    public static IStatEntry CreateStat(Define.Stat stat, Define.StatType type, float value) {
        switch (type) {
            case Define.StatType.Flat:
                return CreateStat(stat, value);
            case Define.StatType.Bounded:
                return CreateBoundedStat(stat, value);
            case Define.StatType.Group:
                return CreateStatGroup(stat, value);
            default:
                return null;
        }
    }


    public static StatGroup CreateStatGroup(Define.Stat targetStat, float maxValue) {
        return new StatGroup(
            targetStat.ToID(),
            new BoundedStat(StatGroup.CURRENT_VALUE, maxValue),
            new Stat(StatGroup.MAX_VALUE, maxValue));
    }
    public static Stat CreateStat(Define.Stat targetStat, float value) {
        return new Stat(targetStat.ToID(), value);
    }
    public static Stat CreateBoundedStat(Define.Stat targetStat, float maxValue) {
        return new BoundedStat(targetStat.ToID(), maxValue);
    }
}
