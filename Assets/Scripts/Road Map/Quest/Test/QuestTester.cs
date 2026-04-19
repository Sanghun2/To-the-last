using System;
using UnityEngine;

public class QuestTester : MonoBehaviour
{
    [SerializeField] QuestSD testQuestSD;
    [SerializeField] int taskCount;
    [SerializeField] Quest currentQuest;

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
}
