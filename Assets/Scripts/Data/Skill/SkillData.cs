using System;
using UnityEngine;

[Serializable]
public class SkillData
{
    public string SkillID => skillID;

    [SerializeField] string skillID;

    public SkillData(string iD) {
        skillID = iD;
    }
}
