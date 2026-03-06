using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillContainer : IInitializable
{
    [SerializeField] List<SkillData> skillList;
    [SerializeField] private int skillCountLimit = 4;

    private bool _isInit;

    public bool IsInit => _isInit;

    public void Init() {
        if (IsInit) return;

        skillList = new List<SkillData>(skillCountLimit);
        for (int i = 0; i < skillCountLimit; i++) {
            skillList.Add(SkillData.Empty);
        }

        _isInit = true;
    }
    public void Release() {
        skillList = null;
        _isInit = false;
    }

    public void RegisterSkill(int index, SkillData skillData) {
        index = Mathf.Clamp(index, 0, skillCountLimit-1);
        skillList[index] = skillData;
    }
    public void ClearSkill(int index) {
        index = Mathf.Clamp(index, 0, skillCountLimit-1);
        skillList[index] = SkillData.Empty;
    }
}
