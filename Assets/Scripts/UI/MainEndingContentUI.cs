using System;
using System.Collections.Generic;
using BilliotGames;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainEndingContentUI : UIBase
{
    [Header("[  Options  ]")]
    [SerializeField] Color escapedColor;
    [SerializeField] Color failedColor;

    [Space]
    [Header("[  Assigns  ]")]
    [SerializeField] Image mainIconImage;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] CanvasGroup mainGroup;


    [Space]
    [Header("[  Test  ]")]
    [SerializeField] bool test;
    [SerializeField] Color testColor;

    private Dictionary<Define.EndingType, Color> themeColor = new();

    public void ShowUI(string endingID) {
        if (Managers.SD.TryGetSD(endingID, out EndingSD endingSD)) {
            InitUI(endingSD.EndingType, endingSD.IconImage, endingSD.Text);
        }
    }

    private void InitUI(Define.EndingType endingType, Sprite iconImage, string text) {
        mainIconImage.sprite = iconImage;
        mainText.text = text;

        mainIconImage.color = themeColor[endingType];
        mainText.color = themeColor[endingType];

        OpenUI();
    }

    private void Awake() {
        themeColor.Add(Define.EndingType.Esacped, escapedColor);
        themeColor.Add(Define.EndingType.Failed, failedColor);
    }
    private void OnValidate() {
        if (test) {
            mainIconImage.color = testColor;
            mainText.color = testColor;
        }
    }
}
