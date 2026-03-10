using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class TurnUI : UIBase
{
    [SerializeField] TextMeshProUGUI turnText;

    public override void InitUI() {
        if (IsInit) return;

        Managers.Turn.OnTurnChanged -= UpdateUI;
        Managers.Turn.OnTurnChanged += UpdateUI;

        _isInit = true;
    }

    private void UpdateUI(int currentTurn, int prevTurn) {
        turnText.text = $"Turn {currentTurn}";
        //turnText.SetText($"Turn {0}", currentTurn);
    }

    private void Reset() {
        turnText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
