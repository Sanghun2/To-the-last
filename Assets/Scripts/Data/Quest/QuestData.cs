using System.Collections.Generic;
using UnityEngine;

public class QuestData : DataBase
{
    public IReadOnlyList<TaskData> TaskDataList => tasks;
    public Quest.Type Type => type;

    [SerializeField] TaskData[] tasks;
    [SerializeField] Quest.Type type;

    public QuestData(string id, Quest.Type type, TaskData[] tasks) : base(id) {
        this.tasks = tasks;
        this.type = type;
    }
}