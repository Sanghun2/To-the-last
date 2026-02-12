using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CraftStructureUI : UIBase
{
    public RecipeSD TargetRecipeSD => currentRecipeSD;

    [SerializeField] TitleText popUpTitleText;
    [SerializeField] ImageUI itemImageUI;
    [SerializeField] DescriptionText itemDescriptionText;
    [SerializeField] CraftButton craftButton;
    [SerializeField] ProgressBarUI progressBarUI;
    private RecipeSD currentRecipeSD;

    public void SetTitleText(StructureSD structureSD) {
        popUpTitleText.SetText(structureSD.DisplayName);
    }
    public void SetRecipe(RecipeSD recipeSD) {
        currentRecipeSD = recipeSD;
    }
    public void InitSelectedItemData(ItemSD itemSD) {
        itemImageUI.SetImage(itemSD.ItemImage);
        itemDescriptionText.SetText(itemSD.Description);
    }

    public void InitProgressUI(float currentValue, float totalValue) {
        progressBarUI.InitUI(currentValue, totalValue);
    }
    public void UpdateProgressUI(float currentValue, float totalValue) {
        progressBarUI.UpdateUI(currentValue, totalValue);
    }

    private void Reset() {
        if (popUpTitleText == null) {
            popUpTitleText = GetComponentInChildren<TitleText>();
        }

        if (itemDescriptionText == null) {
            itemDescriptionText = GetComponentInChildren<DescriptionText>();
        }

        if (itemImageUI == null) {
            itemImageUI = GetComponentInChildren<ImageUI>();
        }

        if (progressBarUI == null) {
            progressBarUI = GetComponentInChildren<ProgressBarUI>();
        }
    }
}
