using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;

public static partial class Extension
{
    #region Data

    public static ExtendedItemData ToData(this ItemSD itemSD) {
        return new ExtendedItemData(itemSD.ID, itemSD.MaxStackCount, itemSD.Weight);
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

    public static CharacterData ToData(this CharacterSD characterSD) {
        return new CharacterData(
            characterSD.ID,
            characterSD.DisplayText,
            characterSD.Description,
            characterSD.Image,
            characterSD.Features,
            characterSD.IsDefaultCharacter
            );
    }

    public static LocationData ToData(this LocationSD locationSD) {
        return new LocationData(
            locationSD.ID,
            locationSD.LocationEventList,
            locationSD.DisplayText,
            locationSD.StoryDescription,
            locationSD.AnchoredPosition,
            locationSD.MainImage,
            locationSD.IconImage
            );
    }

    public static ItemEventArgs ToArgs(this ItemStack itemStack) {
        return new ItemEventArgs(itemStack.ItemData.ItemID, itemStack.Amount);
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
        statContainer.RegisterStat(new BoundedStat(Define.Stat.Hp.ToID(), 100));
        statContainer.RegisterStat(new BoundedStat(Define.Stat.Hunger.ToID(), 100));
        statContainer.RegisterStat(new BoundedStat(Define.Stat.Thirst.ToID(), 100));
        statContainer.RegisterStat(new BoundedStat(Define.Stat.Mental.ToID(), 100));
        statContainer.RegisterStat(new Stat(Define.Stat.Temperature.ToID(), 36.5f));

        statContainer.RegisterStat(new Stat(Define.Stat.Strength.ToID(), 20));
        statContainer.RegisterStat(new Stat(Define.Stat.Agility.ToID(), 10));
        statContainer.RegisterStat(new Stat(Define.Stat.Toughness.ToID(), 10));
        statContainer.RegisterStat(new Stat(Define.Stat.Focus.ToID(), 20));
    }
    public static void InitStats(this StatContainer statContainer, IReadOnlyList<StatData> statDataList) {
        statContainer.CreateDefaultStats();
        for (int i = 0; i < statDataList.Count; i++) {
            var statData = statDataList[i];
            var statID = statData.Stat.ToID();
            if (!statContainer.TryGetStat(statID, out IStatEntry stat)) { Debug.LogError($"no ({statID}) stat exist"); continue; }

            var originalValue = stat.RawValue;
            var newValue = new Value<float>(statData.Value, 0, originalValue.MinValue, statData.Value);
            if (!statContainer.TryOverrideStatValue(statID, newValue)) {
                Debug.LogError($"<color=red>failed to override stat ({statID})</color>");
            }
        }
    }

    #endregion

    #region Utility

    public static FocusJob WithBlockScreen(this FocusJob focusJob) {
        var newJob = new FocusJob(
            focusJob.TotalMinutes, 
            focusJob.Duration, 
            onStart:() => { Managers.ScreenBlocker.SetActive(true); focusJob.OnStart?.Invoke(); },  
            onProgress:focusJob.OnProgress, 
            onComplete:() => { Managers.ScreenBlocker.SetActive(false); focusJob.OnComplete?.Invoke(); });
        return newJob;
    }

    #endregion
}
