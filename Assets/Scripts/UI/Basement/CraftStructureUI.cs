using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CraftStructureUI : UIBase
{
    [SerializeField] TextUI popUpTitleText;
    [SerializeField] DescriptionUI descriptionUI;
    [SerializeField] ItemButtonContainer itemButtonContainer;
    [SerializeField] CraftButton craftButton;
    [SerializeField] ContentUI selectedItemUI;
    [SerializeField] ProgressBarUI progressBarUI;

    public override void InitUI() {
        if (IsInit) return;

        itemButtonContainer.InitUI();
        Managers.Craft.OnTargetSet -= UpdateSelectedRecipe;
        Managers.Craft.OnTargetSet += UpdateSelectedRecipe;

        _isInit = true;
    }

    public void ShowList(IReadOnlyList<RecipeSD> recipes) {
        itemButtonContainer.ShowList(recipes);
        UpdateSelectedRecipe(recipes.First());
    }
    public void SetTitleText(StructureSD structureSD) {
        popUpTitleText.SetText(structureSD.DisplayName);
    }

    public void InitProgressUI(float currentValue, float totalValue) {
        progressBarUI.InitUI(currentValue, totalValue);
    }
    public void UpdateProgressUI(float currentValue, float totalValue) {
        progressBarUI.UpdateUI(currentValue, totalValue);
    }

    private void Reset() {
        if (popUpTitleText == null) {
            popUpTitleText = GetComponentInChildren<TextUI>();
        }

        if (progressBarUI == null) {
            progressBarUI = GetComponentInChildren<ProgressBarUI>();
        }
    }
    private void ShowDescription(RecipeSD recipeSD) {
        descriptionUI.InitContent(recipeSD);
    }

    private void UpdateSelectedRecipe(RecipeSD recipeSD) {
        if (recipeSD == null) { Debug.LogError($"<color=red>recipe null은 의도하지 않은 동작</color>"); return; }
        ShowDescription(recipeSD);
        craftButton.SetButtonText($"제작 ({recipeSD.RequireMinutes}분)");
        selectedItemUI.SetContentImage(recipeSD.IconImage);
    }
}
