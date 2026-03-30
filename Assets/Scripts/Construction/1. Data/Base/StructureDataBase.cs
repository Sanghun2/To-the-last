using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public abstract class StructureDataBase
{
    public string ID => id;
    public IReadOnlyList<Ingredient> RequirementItems => requirementItems;
    public int ConstructionTime => constructionTime;
    public Sprite StructureImage => structureImage;
    public string DisplayText => displayText;

    private string id;
    private string displayText;
    private int constructionTime;
    private IReadOnlyList<Ingredient> requirementItems;
    private Sprite structureImage;

    public StructureDataBase(
        string id,
         string displayText,
        Sprite structureImage,
        int constructionTime, 
        IReadOnlyList<Ingredient> requirementItems
       ) {

        this.id = id;
        this.displayText = displayText;
        this.constructionTime = constructionTime;
        this.requirementItems = requirementItems;
        this.structureImage = structureImage;
    }
}
