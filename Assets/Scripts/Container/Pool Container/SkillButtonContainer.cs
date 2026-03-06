using System;
using UnityEngine;

public class SkillButtonContainer : ListContainerBase<SkillButton>
{
    internal void Clear() {
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].Return();
        }
    }
}
