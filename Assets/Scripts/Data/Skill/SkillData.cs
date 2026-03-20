using System;
using UnityEngine;

[Serializable]
public class SkillData : DataBase
{
    public string SkillID => skillID;
    public SkillSD SkillSD => skillSD;

    public static SkillData Empty
    {
        get
        {
            return new SkillData(string.Empty);
        }
    }

    [SerializeField] string skillID;
    protected SkillSD skillSD;

    public SkillData(SkillSD skillSD) : base(skillSD.ID) {
        this.skillSD = skillSD;
    }

    public SkillData(string id) : base(id) {
    }
}
