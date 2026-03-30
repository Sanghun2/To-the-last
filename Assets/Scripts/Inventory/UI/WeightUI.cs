using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class WeightUI : UIBase
{
    [SerializeField] TextMeshProUGUI weightText;
    private WeightCounter counter;

    public void SetWeightCounter(WeightCounter weightCounter) {
        InitUI();
        counter = weightCounter;

        if (counter != null) {
            Debug.Log("counter set");
            counter.OnWeightChanged -= UpdateWeightUI;
            counter.OnWeightChanged += UpdateWeightUI;
            UpdateWeightUI(counter.CurrentWeight, counter.LimitWeight, 0);
        }
    }
    public void UpdateWeightUI(int currentWeight, int limitWeight, int prevWeight) {

        weightText.SetText("{0} / {1}", currentWeight, limitWeight);
        weightText.color = currentWeight >= limitWeight ? Color.orange : Color.white;
    }


    private void OnDisable() {
        if (counter != null) {
            counter.OnWeightChanged -= UpdateWeightUI;
            counter = null;
        }
    }
}
