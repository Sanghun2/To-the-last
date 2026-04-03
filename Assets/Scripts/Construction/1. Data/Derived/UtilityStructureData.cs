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
        IReadOnlyList<ActivityContentSD> contentList,
        string defaultExecutionButtonText
        ) 
        : base(id, categoryID, displayText, structureImage, constructionTime, requirementItems, defaultExecutionButtonText) {

        this.contentList = contentList;
    }

    public IReadOnlyList<ActivityContentSD> ContentList => contentList;

    private IReadOnlyList<ActivityContentSD> contentList;
}
