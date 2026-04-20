using System;
using BilliotGames;
using UnityEngine;

public class QuestTester : MonoBehaviour
{
    [SerializeField] QuestSD testQuestSD;
    [SerializeField] int taskCount;
    [SerializeField] Quest currentQuest;
    [SerializeField] ItemSD testTargetItem;

    public void CompleteTask() {
        if (currentQuest.TryCompleteCurrentTask()) {
            Debug.Log($"task complete");
        }
        else {
            Debug.Log($"failed to complete task");
        }
    }
    public void AddTaskCount() {
        currentQuest.CurrentTask.TryAddCount(taskCount);
    }

    public void PublishTestQuest() {
        currentQuest = new Quest(testQuestSD.ToData());
        currentQuest.StartQuest();
    }

    public void RemoveTaskCount() {
        currentQuest.CurrentTask.TryRemoveCount(Mathf.Abs(taskCount));
    }

    public void AddTestItem() {
        if (Managers.Inventory.TryGetInventoryByTag(Define.Tag.PLAYER, out var list)) {
            list.TryPushItem(new ItemStack(new ItemData(testTargetItem.ID, testTargetItem.MaxStackCount), taskCount), true);
            Debug.Log($"add item. current? {list[0].GetItemCount(testTargetItem.ID)}");
        }
        else {
            Debug.Log($"add failed");
        }
    }
    public void LogCurrentItem() {
        if (Managers.Inventory.TryGetInventoryByTag(Define.Tag.PLAYER, out var list)) {
            Debug.Log($"current? {list[0].GetItemCount(testTargetItem.ID)}");
        }
    }
}
