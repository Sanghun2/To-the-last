using System.Collections.Generic;
using UnityEngine;

public class QuestData : BaseData
{
    public IReadOnlyList<TaskInfo> TaskInfos => taskInfos;
    public Quest.Type Type => type;

    [SerializeField] TaskInfo[] taskInfos;
    [SerializeField] Quest.Type type;

    public QuestData(string id, Quest.Type type, TaskInfo[] tasks) : base(id) {
        this.taskInfos = tasks;
        this.type = type;
    }
}