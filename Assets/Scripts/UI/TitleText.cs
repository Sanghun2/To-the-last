using BilliotGames;
using TMPro;
using UnityEngine;

public class TitleText : UIBase
{
    [SerializeField] TextMeshProUGUI titleText;

    public void SetText(string titleName) {
        titleText.text = titleName;
    }

    private void Reset() {
        if (titleText == null) {
            titleText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
