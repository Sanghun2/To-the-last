using System;
using UnityEngine;

[Serializable]
public class SkillData
{
    public string SkillID => skillID;
    public SkillSD SkillSD => skillSD;

    [SerializeField] string skillID;
    protected SkillSD skillSD;

    public SkillData(string iD) {
        skillID = iD;
    }

    public SkillData(SkillSD skillSD) {
        this.skillID = skillSD.ID;
        this.skillSD = skillSD;
    }
}
