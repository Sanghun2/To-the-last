using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest
{
    public enum Type {
        Main,
        Sub,
        Achievement,
    }

    public QuestData Data => questData;
    public IReadOnlyList<Task> TaskList => taskList;

    [SerializeField] QuestData questData;
    [SerializeField] List<Task> taskList;

    public Quest(QuestData data) {
        questData = data;
        taskList = data.TaskInfos.Select(t => new Task(t)).ToList();
    }
}
