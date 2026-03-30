using System.Collections.Generic;
using UnityEngine;

public class ProductionStructureData : StructureDataBase
{
    public IReadOnlyList<RecipeSD> Prouctions => prodictionList;

    private IReadOnlyList<RecipeSD> prodictionList;

    public ProductionStructureData(
        string id, 
        string displayText,
        Sprite sturctureImage, 
        int constructionTime, 
        IReadOnlyList<Ingredient> requirementItems,
        IReadOnlyList<RecipeSD> prodictionList) 
        : base(id, displayText, sturctureImage, constructionTime, requirementItems) {

        this.prodictionList = prodictionList;
    }
}
