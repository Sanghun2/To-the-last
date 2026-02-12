using BilliotGames;
using UnityEngine;

public class DescriptionUI : UIBase
{
    [SerializeField] TextUI itemNameText;
    [SerializeField] ContentUI itemContentUI;
    [SerializeField] RequirementUIContainer requirementUIContainer;
    [SerializeField] TextUI descriptionText;

    public void InitContent(RecipeSD recipeSD) {
        itemNameText.SetText(recipeSD.DisplayName);
        itemContentUI.SetContentImage(recipeSD.IconImage);
        requirementUIContainer.ShowList(recipeSD.Inputs);
        descriptionText.SetText(recipeSD.Description);
    }
}
