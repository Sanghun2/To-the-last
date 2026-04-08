using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSD", menuName = "Scriptable Objects/SkillSD")]
public class SkillSD : ImageSDBase
{
    public IReadOnlyList<Effect> Effects => effects;
    public StrategyBehaviour.BehaviourType BehaviourType => behaviourType;
    public Define.ActionAnimationType AnimationType => animationType;
    public int Level => level;

    [Space]
    [SerializeField] StrategyBehaviour.BehaviourType behaviourType = StrategyBehaviour.BehaviourType.Normal;
    [SerializeField] Define.ActionAnimationType animationType;

    [SerializeField] int level;
    [Tooltip("필요한 생존 포인트")]
    [SerializeField] int requireSurvivalPoint;
    [Tooltip("레벨 업에 필요한 포인트")]
    [SerializeField] int requirePointToLevelUp;

    [Space]
    [SerializeField] TriggerSD[] triggers;
    [SerializeField] Condition[] conditions;
    [SerializeField] Effect[] effects;

    [Space]
    [SerializeField] List<PrequisiteSkill> prequisiteSkillList;
    [SerializeField] List<UnlockSkill> unlockSkillList;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_SkillSD");
    }
}


[Serializable]
public class PrequisiteSkill
{
    public SkillSD SkillSD => skillSD;
    public int NeedLevel => needLevel;

    [SerializeField] SkillSD skillSD;
    [SerializeField] int needLevel;
}

[Serializable]
public class UnlockSkill
{
    [SerializeField] SkillSD skillSD;
    [SerializeField] int requierCurrentSkillLevel;
}