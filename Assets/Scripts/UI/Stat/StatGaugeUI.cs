using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class StatGaugeUI : StatUIBase
{
    [SerializeField] protected Image statFillImage;

    public override void UpdateUI(Value value) {
        if (value.MaxValue == 0) { Debug.LogAssertion($"max value 0. func returned"); return; }
        statFillImage.fillAmount = value.CurrentValue / value.MaxValue;
    }
}
