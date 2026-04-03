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
    public string CategoryID => categoryID;
    public string DefaultExecutionButtonText => defaultExecutionButtonText;


    private string id;
    private string displayText;
    private int constructionTime;
    private IReadOnlyList<Ingredient> requirementItems;
    private Sprite structureImage;
    private string categoryID;
    private string defaultExecutionButtonText;

    public StructureDataBase(
        string id,
        string categoryID,
        string displayText,
        Sprite structureImage,
        int constructionTime, 
        IReadOnlyList<Ingredient> requirementItems,
        string defaultExecutionButtonText
       ) {

        this.id = id;
        this.categoryID = categoryID;
        this.displayText = displayText;
        this.constructionTime = constructionTime;
        this.requirementItems = requirementItems;
        this.structureImage = structureImage;
        this.defaultExecutionButtonText = defaultExecutionButtonText;
    }
}
