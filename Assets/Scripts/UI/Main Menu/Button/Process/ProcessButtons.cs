using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class ProcessButtons : UIBase
{
    [SerializeField] TextMeshProUGUI nextProcessButtonText;
    [SerializeField] TextMeshProUGUI prevProcessButtonText;

    public bool IsFirstProcess => Managers.Process.CurrentChain?.IsFirstProcess == true;
    public bool IsLastProcess => Managers.Process.CurrentChain?.IsLastProcess == true;


    private void OnEnable() {
        if (Managers.Process.CurrentChain != null) {
            Managers.Process.CurrentChain.OnProcessChanged -= UpdateUI;
            Managers.Process.CurrentChain.OnProcessChanged += UpdateUI;
        }
    }

    private void OnDisable() {
        if (Managers.Process.CurrentChain != null) {
            Managers.Process.CurrentChain.OnProcessChanged -= UpdateUI;
            Managers.Process.CurrentChain.OnProcessChanged += UpdateUI;
        }
    }


    private void UpdateUI(int _, int __) {
        Debug.Log($"process index? {Managers.Process.CurrentChain?.CurrentProcessIndex}");
        nextProcessButtonText.text = IsLastProcess ? "출발" : "결정";
        prevProcessButtonText.text = IsFirstProcess ? "포기" : "이전";
    }
}
