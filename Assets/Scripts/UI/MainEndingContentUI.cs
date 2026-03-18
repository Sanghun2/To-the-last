using System;
using BilliotGames;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainEndingContentUI : UIBase
{
    [Header("[  Options  ]")]
    [SerializeField] Color escapedColor;
    [SerializeField] Color deadColor;

    [Space]
    [Header("[  Assigns  ]")]
    [SerializeField] Image mainIconImage;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] CanvasGroup mainGroup;


    [Space]
    [Header("[  Test  ]")]
    [SerializeField] bool test;
    [SerializeField] Color testColor;



    public void ShowUI(string endingID) {
        if (Managers.SD.TryGetSD(endingID, out EndingSD endingSD)) {
            InitUI(endingSD.IconImage, endingSD.Text);
        }
    }

    private void InitUI(Sprite iconImage, string text) {
        mainIconImage.sprite = iconImage;
        mainText.text = text;
    }

    private void OnValidate() {
        if (test) {
            mainIconImage.color = testColor;
            mainText.color = testColor;
        }
    }
}
