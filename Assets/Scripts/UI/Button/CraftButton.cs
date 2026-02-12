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
    public CraftStructureUI CachedCraftUI
    {
        get
        {
            if (_craftUI == null) {
                _craftUI = Managers.UI.GetUI<CraftStructureUI>();
            }

            return _craftUI;
        }
    }

    [SerializeField] TextMeshProUGUI buttonText;
    private CraftStructureUI _craftUI;

    public void SetButtonText(string text) {
        buttonText.text = text; 
    }

    protected override void ButtonAction() {
        if (Managers.Job.IsFocusJobRunning) return;

        CraftStructureUI craftUI = Managers.UI.GetUI<CraftStructureUI>();
        var targetRecipeSD = craftUI.TargetRecipeSD;
        craftUI.InitProgressUI(0, targetRecipeSD.RequireMinutes);
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
        CachedCraftUI.UpdateProgressUI(currentValue, totalValue);
    }
}
