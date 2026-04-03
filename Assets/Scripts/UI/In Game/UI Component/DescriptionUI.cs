using BilliotGames;
using UnityEngine;

public class DescriptionUI : UIBase
{
    [SerializeField] TextUI itemNameText;
    [SerializeField] ContentUI itemContentUI;
    [SerializeField] RequirementUIContainer requirementUIContainer;
    [SerializeField] TextUI descriptionText;

    public void InitContent(ProductionContentSD recipeSD) {
        itemNameText.SetText(recipeSD.DisplayText);
        itemContentUI.SetContentImage(recipeSD.Image);
        requirementUIContainer.ShowRequirements(recipeSD.Requirements);
        descriptionText.SetText(recipeSD.Description);
    }
}
