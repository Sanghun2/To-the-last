using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

public static partial class Extension
{
    #region Data

    public static ItemData ToData(this ItemSD itemSD) {
        return new ItemData(itemSD.ID, itemSD.MaxStackCount);
    }
    public static SkillData ToData(this SkillSD skillSD) {
        return new SkillData(skillSD.ID);
    }
    public static StoryData ToData(this StorySD storySD) {
        var newData = new StoryData(storySD.ID);
        return newData;
    }
    public static TaskData ToData(this TaskSD taskSD) {
        return new TaskData(taskSD.ID, taskSD.CountType, taskSD.RequireCount);
    }
    public static QuestData ToData(this QuestSD questSD) {
        TaskData[] tasks = questSD.TaskSDs.Select(tSD => tSD.ToData()).ToArray();
        return new QuestData(questSD.ID, questSD.Type, tasks);
    }

    public static TraitData ToData(this TraitSD traitSD) {
        return new TraitData(
            traitSD.ID, 
            traitSD.DisplayText, 
            traitSD.Description, 
            traitSD.Image,
            traitSD.Cost);
    }

    #endregion


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

    #region Stat

    public static void CreateDefaultStats(this StatContainer statContainer) {
        statContainer.ClearStats();
        statContainer.RegisterStat(Define.Stat.Hp.ToID(), new BoundedStat(100));
        statContainer.RegisterStat(Define.Stat.Hunger.ToID(), new BoundedStat(100));
        statContainer.RegisterStat(Define.Stat.Thirst.ToID(), new BoundedStat(100));
        statContainer.RegisterStat(Define.Stat.Mental.ToID(), new BoundedStat(100));
        statContainer.RegisterStat(Define.Stat.Temperature.ToID(), new Stat(36.5f));

        statContainer.RegisterStat(Define.Stat.Strength.ToID(), new Stat(20));
        statContainer.RegisterStat(Define.Stat.Agility.ToID(), new Stat(10));
        statContainer.RegisterStat(Define.Stat.Toughness.ToID(), new Stat(10));
        statContainer.RegisterStat(Define.Stat.Focus.ToID(), new Stat(20));
    }
    public static void InitStats(this StatContainer statContainer, IReadOnlyList<StatData> statDataList) {
        statContainer.CreateDefaultStats();
        for (int i = 0; i < statDataList.Count; i++) {
            var statData = statDataList[i];
            var statID = statData.Stat.ToID();
            if (!statContainer.TryGetStat(statID, out Stat stat)) { Debug.LogError($"no ({statID}) stat exist"); continue; }

            var originalValue = stat.RawValue;
            var newValue = new Value<float>(statData.Value, 0, originalValue.MinValue, statData.Value);
            if (!statContainer.TryOverrideStatValue(statID, newValue)) {
                Debug.LogError($"<color=red>failed to override stat ({statID})</color>");
            }
        }
    }

    #endregion

}
