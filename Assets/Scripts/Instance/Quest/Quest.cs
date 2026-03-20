using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public enum Type {
        Story,
        Side,
    }

    [SerializeField] QuestData questData;
    [SerializeField] List<Task> taskList = new List<Task>();

    public Quest(QuestData data) {
        questData = data;
        InitTask(data.TaskDataList);
    }

    private void InitTask(IReadOnlyList<TaskData> taskDataList) {
        for (int i = 0; i < taskDataList.Count; i++) {
            TaskData taskData = taskDataList[i];
            taskList.Add(new Task(taskData));
        }
    }
}
