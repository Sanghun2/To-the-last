using System;
using System.Security.Cryptography;
using BilliotGames;
using TMPro;
using UnityEngine;

public interface IProgressor
{
    void UpdateProgressor(float currentValue, float totalValue);
}

public class CraftButton : ButtonBase
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

    public override void InitUI() {
        if (IsInit) return;
        base.InitUI();

        Managers.Craft.CraftContext.OnStateChanged -= UpdateState;
        Managers.Craft.CraftContext.OnStateChanged += UpdateState;

        _isInit = true;
    }

    

    public void SetButtonText(string text) {
        buttonText.text = text; 
    }

    protected override void ButtonAction() {

    }

    private void Craft() {
        if (Managers.Job.IsFocusJobRunning) return;

        var targetRecipeSD = Managers.Craft.CraftTarget;

        if (Managers.Craft.TryCraft(targetRecipeSD,
            CachedCraftUI.UpdateProgressUI,
            () => {
                CachedCraftUI.InitProgressUI(0, 1);
                Managers.Craft.RegisterDelayedJob(
                targetRecipeSD,
                CachedCraftUI.UpdateProgressUI);
            })) {
            CachedCraftUI.InitProgressUI(0, 1);
        }
    }
    private void ClaimResult() {
        Managers.Craft.ClaimCraftResult();
        CachedCraftUI.InitProgressUI(0, 1);
    }

    protected override void Reset() {
        base.Reset();

        if (buttonText == null) {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    private void UpdateState(CraftContext.State currentState, CraftContext.State state2) {
        UpdateAction(currentState);
        UpdateButtonText(currentState);
    }

    private void UpdateAction(CraftContext.State currentState) {
        switch (currentState) {
            case CraftContext.State.None:
                break;
            case CraftContext.State.Selected:
                SetButtonAction(Craft);
                break;
            case CraftContext.State.Crafting:
                break;
            case CraftContext.State.Completed:
                SetButtonAction(ClaimResult);
                break;
            default:
                break;
        }
    }

    private void UpdateButtonText(CraftContext.State currentState) {
        switch (currentState) {
            case CraftContext.State.None:
                break;
            case CraftContext.State.Selected:
                break;
            case CraftContext.State.Crafting:
                buttonText.text = "제작중";
                break;
            case CraftContext.State.Completed:
                buttonText.text = "획득";
                break;
            default:
                break;
        }
    }
}
