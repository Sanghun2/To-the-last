using System.Collections.Generic;
using UnityEngine;

public class LootSelectionRunnerData : SelectionRunnerDataBase
{
    public LootSelectionRunnerData(
        int requireMinutes, 
        string displayText, 
        Define.RequirementType requirementType, 
        Ingredient requirement) 
        : base(requireMinutes, displayText, requirementType, requirement) {


    }
}
