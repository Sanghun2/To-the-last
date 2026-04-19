using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData : BaseData
{
    public IReadOnlyList<TaskInfo> TaskInfos => taskInfos;
    public Quest.Type Type => type;

    [SerializeField] IReadOnlyList<TaskInfo> taskInfos;
    [SerializeField] Quest.Type type;

    public QuestData(string id, Quest.Type type, IReadOnlyList<TaskInfo> tasks) : base(id) {
        this.taskInfos = tasks;
        this.type = type;
    }
}