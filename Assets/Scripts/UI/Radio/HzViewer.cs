using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class HzViewer : UIBase
{
    [SerializeField] TextMeshProUGUI hzValueText;

    public void UpdateHz(float currentHz) {
        hzValueText.SetText("{0.0}", currentHz);
    }
}
