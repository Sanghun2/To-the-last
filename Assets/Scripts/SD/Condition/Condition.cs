using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Condition
{
    public ImageSDBase RequiredTarget => requireTarget;
    public Define.RequirementType RequirementType => requirementType;
    public int RequirementAmount => requiredAmount;

    [SerializeField] Define.RequirementType requirementType;
    [SerializeField] protected ImageSDBase requireTarget;
    [SerializeField] int requiredAmount;
}
