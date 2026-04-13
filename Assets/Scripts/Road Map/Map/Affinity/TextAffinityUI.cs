using TMPro;
using UnityEngine;

public class TextAffinityUI : AffinityUIBase
{
    [SerializeField] TextMeshProUGUI affinityText;

    public override void UpdateUI(float currentAffinity, float maxAffinity) {
        affinityText.SetText("우호도 {0}/{1}", currentAffinity, maxAffinity);
    }
}
