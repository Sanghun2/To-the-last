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

    //public override void InitUI() {
    //    if (IsInit) return;
    //    base.InitUI();

    //    Managers.Craft.CraftContext.OnLocationStateChanged -= UpdateState;
    //    Managers.Craft.CraftContext.OnLocationStateChanged += UpdateState;

    //    _isInit = true;
    //}

    

    public void SetButtonText(string text) {
        buttonText.text = text; 
    }

    protected override void ButtonAction() {

    }

    //private void Craft() {
    //    if (Managers.Job.IsFocusJobRunning) return;

    //    var targetProdiction = Managers.Craft.CraftTarget;

    //    if (!Managers.Craft.TryCraft(targetProdiction, () => {
    //            Managers.Craft.RegisterDelayedJob(targetProdiction);
    //        }
    //        )) {
    //        Debug.LogError($"<color=red>failed to try craft item</color>");
    //    }
    //}
    //private void ClaimResult() {
    //    Managers.Craft.ClaimCraftResult();
    //}

    protected override void Reset() {
        base.Reset();

        if (buttonText == null) {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    //private void UpdateState(CraftContext.LocationState currentState, CraftContext.LocationState state2) {
    //    UpdateAction(currentState);
    //    UpdateButtonText(currentState);
    //}

    //private void UpdateAction(CraftContext.LocationState currentState) {
    //    switch (currentState) {
    //        case CraftContext.LocationState.None:
    //            break;
    //        case CraftContext.LocationState.Selected:
    //            SetButtonAction(Craft);
    //            break;
    //        case CraftContext.LocationState.Crafting:
    //            break;
    //        case CraftContext.LocationState.Completed:
    //            SetButtonAction(ClaimResult);
    //            break;
    //        default:
    //            break;
    //    }
    //}

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
