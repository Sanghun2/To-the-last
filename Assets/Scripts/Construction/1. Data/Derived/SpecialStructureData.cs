using System.Collections.Generic;
using UnityEngine;

public class SpecialStructureData : StructureDataBase
{
    public Structure.SpecialStructureType StructureType { get; }

    public SpecialStructureData(
        string id, 
        string categoryID,
        string displayText,
        Sprite structureImage,
        int constructionTime,
        IReadOnlyList<Ingredient> requirementItems,
        string defaultExecutionButtonText,
        Structure.SpecialStructureType structureType)
        : base(id, categoryID, displayText, structureImage, constructionTime, requirementItems, defaultExecutionButtonText) {

        StructureType = structureType;
    }
}
