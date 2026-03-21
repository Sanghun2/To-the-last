using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class TraitPointView : UIBase
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] Color positiveColor;
    [SerializeField] Color negativeColor;

    [Space]
    [Header("[  Test  ]")]
    [SerializeField] bool test;
    [SerializeField] Color testColor;

    public void SetPointText(int point) {
        pointText.color = GetColor(point);
        pointText.SetText("Point: {0}", point);
    }

    private Color GetColor(int point) {
        return point >= 0 ? positiveColor : negativeColor;
    }

#if UNITY_EDITOR
    private void OnValidate() {
        if (test) {
            pointText.color = testColor;
        }
    }
#endif
}
