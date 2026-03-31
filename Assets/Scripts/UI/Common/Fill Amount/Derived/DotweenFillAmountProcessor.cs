using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DotweenFillAmountProcessor : FillAmountProcessorBase
{
    private Ease progressEase;
    private float tweenDuration = 0.25f;
    private Tweener progressTweener;

    public override void Clear(Image processImage) {
        progressTweener?.Kill();
        base.Clear(processImage);
    }

    public override void UpdateFillAmount(Image processImage, float currentValue, float maxValue) {
        float rate = currentValue / maxValue;
        if (progressTweener != null && progressTweener.IsActive()) {
            progressTweener.ChangeEndValue(rate, tweenDuration, true);
        }
        else {
            progressTweener = processImage
                .DOFillAmount(rate, tweenDuration)
                .SetEase(progressEase);
        }
    }
}
