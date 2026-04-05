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

    //    Managers.Craft.CraftContext.OnStateChanged -= UpdateState;
    //    Managers.Craft.CraftContext.OnStateChanged += UpdateState;

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
    //private void UpdateState(CraftContext.State currentState, CraftContext.State state2) {
    //    UpdateAction(currentState);
    //    UpdateButtonText(currentState);
    //}

    //private void UpdateAction(CraftContext.State currentState) {
    //    switch (currentState) {
    //        case CraftContext.State.None:
    //            break;
    //        case CraftContext.State.Selected:
    //            SetButtonAction(Craft);
    //            break;
    //        case CraftContext.State.Crafting:
    //            break;
    //        case CraftContext.State.Completed:
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
