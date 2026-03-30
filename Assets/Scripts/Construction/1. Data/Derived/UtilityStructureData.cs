using System.Collections.Generic;
using UnityEngine;

public class UtilityStructureData : StructureDataBase
{
    public UtilityStructureData(
        string id, 
        string displayText,
        Sprite structureImage,
        int constructionTime, 
        IReadOnlyList<Ingredient> requirementItems) 
        : base(id, displayText, structureImage, constructionTime, requirementItems) {
    }
}
