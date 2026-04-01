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
    [SerializeField] GameObject lockObj;
    private UtilityContentSD contentSD;

    public void InitContent(UtilityContentSD content) {
        contentSD = content;
        progressBarUI.Clear();
        contentImage.sprite = content.Image;
        nameText.text = content.DisplayText;
        executionButton.SetAction(Execute);

        int structureLevel = Managers.Structure.CurrentSelctedStructure.Level;
        bool @lock = content.RequiredLevel > structureLevel;
        lockObj.SetActive(@lock);
    }

    private void Execute() {
        if (contentSD == null) { Debug.Log("content is null"); return; }

        var job = Managers.Job.CreateFocusJob(
            contentSD.RequireMinutes,
            onProgress: progressBarUI.UpdateUI,
            onComplete: () => {
                var effects = contentSD.Effects;
                for (int i = 0; i < effects.Count; i++) {
                    Effect effect = effects[i];
                    Entity caster = Managers.Player.PlayerData.Entity;
                    Managers.Effect.ApplyEffect(new EffectApplyRequest(effect, caster));
                }
            }).WithBlockScreen();

        Managers.Job.DoFocusJob(job, OnComplete);
    }

    private void OnComplete() {
        progressBarUI.Clear();
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
