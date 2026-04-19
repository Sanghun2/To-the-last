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
        return new TaskData(taskSD.ID, taskSD.CountType);
    }
    //public static TaskData ToData(this TaskInfo taskInfo) {
    //    return new TaskData(taskInfo.TaskSD.ID, taskInfo.TaskSD.CountType);
    //}
    public static QuestData ToData(this QuestSD testQuestSD) {
        return new QuestData(testQuestSD.ID, testQuestSD.Type, testQuestSD.TaskInfos);
    }

    public static TraitData ToData(this TraitSD traitSD) {
        return new TraitData(
            traitSD.ID,
            traitSD.DisplayName,
            traitSD.Description,
            traitSD.Image,
            traitSD.Cost);
    }

    public static CharacterData ToData(this CharacterSD characterSD) {
        return new CharacterData(
            characterSD.ID,
            characterSD.DisplayName,
            characterSD.Description,
            characterSD.Image,
            characterSD.Features,
            characterSD.IsDefaultCharacter
            );
    }

    public static DialogPageData ToData(this DialogPageSD dialogSD) {
        return new DialogPageData(
            dialogSD.ID,
            dialogSD.Image,
            dialogSD.TalkerName,
            dialogSD.Description,
            dialogSD.Selections
            );
    }

    public static ItemEventArgs ToArgs(this ItemStack itemStack) {
        return new ItemEventArgs(itemStack.ItemData.ItemID, itemStack.Amount);
    }

    #endregion

    #region Inventory

    public static bool TryPushItem(this IReadOnlyList<InventoryBase> inventories, ItemStack item, bool ignoreConditions) {
        for (int i = 0; i < inventories.Count; i++) {
            var inventory = inventories[i];
            if (inventory.TryPushItem(item, out ItemStack overStack, ignoreConditions)) {
                return true;
            }
        }

        return false;
    }
    public static bool TryRemoveItem(this IReadOnlyList<InventoryBase> inventories, string itemID, int required) {
        // 1. 수량 충분한지 먼저 체크
        int totalCount = 0;
        for (int i = 0; i < inventories.Count; i++) {
            totalCount += inventories[i].GetItemCount(itemID);
            if (totalCount >= required) break;
        }
        if (totalCount < required) { Debug.LogAssertion($"<color=orange>ingredient insufficient. required? {required}. have? {totalCount}</color>"); return false; }

        // 2. 충분하면 순서대로 제거
        int remaining = required;
        for (int i = 0; i < inventories.Count; i++) {
            if (remaining <= 0) break;
            var inventory = inventories[i];
            int available = inventory.GetItemCount(itemID);
            if (available <= 0) continue;

            int toRemove = Mathf.Min(available, remaining);
            inventory.TryRemoveItem(itemID, toRemove);
            remaining -= toRemove;
        }
        return true;
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
            onStart: () => { Managers.ScreenBlocker.SetActive(true); focusJob.OnStart?.Invoke(); },
            onProgress: focusJob.OnProgress,
            onComplete: () => { Managers.ScreenBlocker.SetActive(false); focusJob.OnComplete?.Invoke(); });
        return newJob;
    }
    public static void Shuffle<T>(this List<T> list) {
        for (int i = list.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    #endregion
}
