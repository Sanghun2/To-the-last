using System.Collections.Generic;
using UnityEngine;

public class ExpensionPopUpData : 
    PopUpDataBase, 
    ITitleContent, 
    IDescriptionContent, 
    IRequirementContent
{
    public ExpensionPopUpData(
        string title,
        string description,
        IReadOnlyList<Ingredient> requirements,
        ActionData[] buttonActions) 
        : base(buttonActions) {

        Title = title;
        Description = description;
        Requirements = requirements;
    }

    public string Title { get; }
    public string Description { get; }
    public IReadOnlyList<Ingredient> Requirements { get; }
}
