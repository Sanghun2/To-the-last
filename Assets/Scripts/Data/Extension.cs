using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

public static partial class Extension
{
    public static ItemData ToItemData(this ItemSD itemSD) {
        return new ItemData(itemSD.ID, itemSD.MaxStackCount);
    }

    public static SkillData ToSkillData(this SkillSD skillSD) {
        return new SkillData(skillSD.ID);
    }

    public static string ToID(this Define.Stat statType) {
        return statType.ToString();
    }
    public static string ToID(this StrategyBehaviour.BehaviourType behaviourType) {
        switch (behaviourType) {
            case StrategyBehaviour.BehaviourType.Initiative:
                return "initiativeSkillIcon";
            case StrategyBehaviour.BehaviourType.Counter:
                return "counterSkillIcon";
            case StrategyBehaviour.BehaviourType.Normal:
            default:
                return "normalSkillIcon";
        }
    }
}
