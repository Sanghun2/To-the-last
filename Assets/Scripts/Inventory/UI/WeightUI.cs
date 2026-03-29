using BilliotGames;
using TMPro;
using UnityEngine;

public class WeightUI : UIBase
{
    [SerializeField] TextMeshProUGUI weightText;
    private WeightCounter counter;

    public override void InitUI() {
        if (IsInit) return;

        if (Managers.Inventory.TryGetInventoryByID("player", out var inventory)) {
            counter = ((SimpleInventory)inventory).WeightCounter;
        }

        _isInit = true;
    }

    public void UpdateWeightUI(int currentWeight, int limitWeight, int prevWeight) {

        weightText.SetText("{0} / {1}", currentWeight, limitWeight);
        weightText.color = currentWeight >= limitWeight ? Color.orange : Color.white;
    }

    private void OnEnable() {
        InitUI();
        if (counter != null) {
            counter.OnWeightChanged -= UpdateWeightUI;
            counter.OnWeightChanged += UpdateWeightUI;
            UpdateWeightUI(counter.CurrentWeight, counter.LimitWeight, 0);
        }
    }

    private void OnDisable() {
        if (counter != null) {
            counter.OnWeightChanged -= UpdateWeightUI;
        }
    }
}
