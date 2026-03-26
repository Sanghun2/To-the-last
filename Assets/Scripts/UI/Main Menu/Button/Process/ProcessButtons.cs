using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class ProcessButtons : UIBase
{
    [SerializeField] TextMeshProUGUI nextProcessButtonText;
    [SerializeField] TextMeshProUGUI prevProcessButtonText;

    public bool IsFirstProcess => Managers.Process.CurrentChain?.IsFirstProcess == true;
    public bool IsLastProcess
    {
        get
        {
            var chain = Managers.Process.CurrentChain;
            return chain != null && chain.IsLastProcess == true;
        }
    }


    private void OnEnable() {
        if (Managers.Process.CurrentChain != null) {
            Managers.Process.CurrentChain.OnProcessChanged -= UpdateUI;
            Managers.Process.CurrentChain.OnProcessChanged += UpdateUI;
            UpdateUI(Managers.Process.CurrentChain.CurrentProcessIndex,0);
        }
    }

    private void OnDisable() {
        if (Managers.Process.CurrentChain != null) {
            Managers.Process.CurrentChain.OnProcessChanged -= UpdateUI;
        }
    }

    private void UpdateUI(int currentProcessIndex, int __) {
        prevProcessButtonText.text = IsFirstProcess ? "포기" : "이전";
        nextProcessButtonText.text = IsLastProcess ? "출발" : "결정";
    }
}
