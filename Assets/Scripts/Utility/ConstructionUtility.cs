using System.Collections.Generic;
using UnityEngine;

public class ConstructionUtility
{
    public static float GetDestructionTime(StructureDataBase structureData) {
        return structureData.ConstructionTime;
    }

    public static IReadOnlyList<Ingredient> GetReturnIngredient(StructureDataBase structureData) {
        var requirements = structureData.RequirementItems;
        List<Ingredient> returnList = new List<Ingredient>(requirements.Count);
        for (int i = 0; i < requirements.Count; i++) {
            Ingredient item = requirements[i];
            returnList.Add(new Ingredient(item.ItemSD, item.Amount/2));
        }

        return returnList;
    }
}
