using System;
using BilliotGames;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : UIBase
{
    [Header("[  State  ]")]
    [SerializeField] Progressor progressor = new Progressor();

    [Space]
    [SerializeField] Image progressBar;
    private FillAmountProcessorBase fillAmountProcessor = new DotweenFillAmountProcessor();

    public override void InitUI() {
        if (IsInit) return;
        Clear();
        _isInit = true;
    }

    public void InitUI(float currentValue, float maxValue) {
        progressBar.fillAmount = currentValue / maxValue;
        progressBar.DOKill();
    }

    public void UpdateUI(float currentValue, float maxValue) {
        //progressor.Update(currentValue, maxValue);
        fillAmountProcessor.UpdateFillAmount(progressBar, currentValue, maxValue);
    }

    public void Clear() {
        fillAmountProcessor.Clear(progressBar);
        //progressor.SetCurrentValue(0);
    }
}