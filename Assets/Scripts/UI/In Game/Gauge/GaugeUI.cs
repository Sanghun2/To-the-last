using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : UIBase
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image frontImage;
    [SerializeField] TextMeshProUGUI hpText;

    public void UpdateUI(float currentValue, float maxValue) {
        frontImage.fillAmount = currentValue / maxValue; 
        hpText.SetText("{0}/{1}", currentValue, maxValue);
    }

    public void UpdateUI(Value<float> value) {
        UpdateUI(value.CurrentValue, value.MaxValue);
    }
}
