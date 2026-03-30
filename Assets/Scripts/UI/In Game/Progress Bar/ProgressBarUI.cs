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
    [SerializeField] Ease progressEase;
    [SerializeField] float tweenDuration = 0.25f;
    private Tweener progressTweener;

    public override void InitUI() {
        UpdateUI(0, 1);
        _isInit = true;
    }

    public void InitUI(float currentValue, float maxValue) {
        progressBar.fillAmount = currentValue / maxValue;
        progressBar.DOKill();
    }

    public void UpdateUI(float currentValue, float maxValue) {
        progressor.Update(currentValue, maxValue);
        if (progressTweener != null && progressTweener.IsActive()) {
            progressTweener.ChangeEndValue(progressor.Rate, tweenDuration, true);
        }
        else {
            //progressBar.fillAmount = currentProgress / maxProgress;
            progressTweener = progressBar
                .DOFillAmount(progressor.Rate, tweenDuration)
                .SetEase(progressEase);
        }
    }

    public void Clear() {
        progressTweener?.Kill();
        progressor.SetCurrentValue(0);
        progressBar.fillAmount = 0;
    }
}