using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UtilityContentUI : UIBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] Image contentImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] ProgressBarUI progressBarUI;
    [SerializeField] ExecutionButton executionButton;
    private UtilityContentSD contentSD;

    public void InitContent(UtilityContentSD content) {
        contentSD = content;
        progressBarUI.Clear();
        contentImage.sprite = content.Image;
        nameText.text = content.DisplayText;
        executionButton.SetAction(Execute);
    }

    private void Execute() {
        if (contentSD == null) { Debug.Log("content is null"); return; }

        var job = Managers.Job.CreateFocusJob(
            contentSD.RequireMinutes, 
            progressBarUI.UpdateUI,
            () => {
                var effects = contentSD.Effects;
                for (int i = 0; i < effects.Count; i++) {
                    var effect = effects[i];
                    Managers.Effect.ApplyEffect(effect);
                }
            });

        Managers.Job.DoFocusJob(job);
    }

    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        InitUI();
    }
    public void Return() {
        contentSD = null;
        CloseUI();
    }

    #endregion
}
