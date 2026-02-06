using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : UIBase
{
    [Header("[  State  ]")]
    [SerializeField] Progressor progressor = new Progressor();

    [Space]
    [SerializeField] Image progressBar;

    public override void InitUI() {
        UpdateUI(0, 1);
    }

    public void UpdateUI(float currentValue, float maxValue) {
        progressor.Update(currentValue, maxValue);
        progressBar.fillAmount = progressor.Rate;
    }
}
