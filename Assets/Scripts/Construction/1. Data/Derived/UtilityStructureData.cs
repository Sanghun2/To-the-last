using System.Collections.Generic;
using UnityEngine;

public class UtilityStructureData : StructureDataBase
{
    public UtilityStructureData(
        string id, 
        string categoryID,
        string displayText,
        Sprite structureImage,
        int constructionTime, 
        IReadOnlyList<Ingredient> requirementItems,
        IReadOnlyList<UtilityContentSD> contentList) 
        : base(id, categoryID, displayText, structureImage, constructionTime, requirementItems) {

        this.contentList = contentList;
    }

    public IReadOnlyList<UtilityContentSD> ContentList => contentList;

    private IReadOnlyList<UtilityContentSD> contentList;
}
