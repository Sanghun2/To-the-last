using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSD", menuName = "Scriptable Objects/Quest/QuestSD")]
public class QuestSD : SDBase
{
    public IReadOnlyList<TaskInfo> TaskInfos => taskInfos;
    public IReadOnlyList<RewardInfo> RewardInfos => rewardInfos;
    public Quest.Type Type => type;

    [SerializeField] Quest.Type type;
    [SerializeField] TaskInfo[] taskInfos;
    [SerializeField] RewardInfo[] rewardInfos;
}

[Serializable]
public class TaskInfo
{
    public TaskSD TaskSD => taskSD;
    public SDBase TargetSD => targetSD;
    public int RequiredCount => requiredCount;

    [SerializeField] TaskSD taskSD;
    [SerializeField] SDBase targetSD;
    [SerializeField] int requiredCount;
}

[Serializable]
public class RewardInfo
{
    public RewardSDBase RewardTypeSD => rewardTypeSD;
    public SDBase RewardTarget => rewardTarget;
    public int Amount => amount;
    public int Weight => weight;

    [SerializeField] RewardSDBase rewardTypeSD;
    [SerializeField] SDBase rewardTarget;
    [SerializeField] int amount;
    [SerializeField] int weight;
}
