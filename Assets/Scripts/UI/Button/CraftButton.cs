using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public interface IProgressor
{
    void UpdateValue(float currentValue, float totalValue);
}

public class CraftButton : ButtonBase, IProgressor
{
    public CraftUI CraftUI
    {
        get
        {
            if (_craftUI == null) {
                _craftUI = Managers.UI.GetUI<CraftUI>();
            }

            return _craftUI;
        }
    }

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] RecipeSD targetRecipeSD;
    private CraftUI _craftUI;

    public void SetText(string text) {
        buttonText.text = text; 
    }
    public void SetRecipe(RecipeSD recipeSD) {
        targetRecipeSD = recipeSD;
    }

    protected override void ButtonAction() {
        if (Managers.Job.IsFocusJobRunning) return;

        var craftUI = Managers.UI.GetUI<CraftUI>();
        var newJob = new FocusJob(targetRecipeSD.RequireMinutes, UpdateValue);
        Managers.Job.DoFocusJob(newJob);
    }

    protected override void Reset() {
        base.Reset();

        if (buttonText == null) {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void UpdateValue(float currentValue, float totalValue) {
        CraftUI.UpdateProgressUI(currentValue, totalValue);
    }
}
