using UnityEngine;
using UnityEngine.UI;

public class LinearFillAmountProcessor : FillAmountProcessorBase
{
    public override void UpdateFillAmount(Image progressImage, float currentValue, float maxValue) {
        progressImage.fillAmount = currentValue / maxValue;
    }
}
