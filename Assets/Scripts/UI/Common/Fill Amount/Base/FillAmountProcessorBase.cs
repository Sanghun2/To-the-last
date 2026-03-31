using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class FillAmountProcessorBase
{
    public abstract void UpdateFillAmount(Image progressImage, float currentValue, float maxValue);
    public virtual void Clear(Image progressImage) {
        progressImage.fillAmount = 0;
    }
}
