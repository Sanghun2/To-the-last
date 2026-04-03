using System.Collections.Generic;
using UnityEngine;

public class ProductionStructureData : StructureDataBase
{
    public IReadOnlyList<ProductionContentSD> Prouctions => prodictionList;

    private IReadOnlyList<ProductionContentSD> prodictionList;

    public ProductionStructureData(
        string id, 
        string categoryID,
        string displayText,
        Sprite sturctureImage, 
        int constructionTime, 
        IReadOnlyList<Ingredient> requirementItems,
        IReadOnlyList<ProductionContentSD> prodictionList) 
        : base(id, categoryID, displayText, sturctureImage, constructionTime, requirementItems) {

        this.prodictionList = prodictionList;
    }
}
