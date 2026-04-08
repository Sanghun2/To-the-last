using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Condition
{
    public SDBase RequireSD => requireSD;
    public Define.RequirementType RequirementType => requirementType;
    public int RequirementAmount => requiredAmount;

    [SerializeField] Define.RequirementType requirementType;
    [SerializeField] protected SDBase requireSD;
    [SerializeField] int requiredAmount;
}
