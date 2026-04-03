using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UtilityContentUI : ContentUIBase<UtilityContentSD>
{
    [SerializeField] protected TextMeshProUGUI activityNameText;
    [SerializeField] protected RequirementUIContainer requirementUIContainer;

    public override void InitContent(UtilityContentSD contentSD) {
        base.InitContent(contentSD);

        var requirements = contentSD.Requirements;
        SetActivityNameText((requirements == null || requirements.Count == 0) ? contentSD.DisplayText : null);
        SetRequirements(requirements);
    }

    protected override void ExecuteButtonAction(int requireMinutes) {
        if (contentSD == null) { Debug.Log("content is null"); return; }

        base.ExecuteButtonAction(requireMinutes);
    }
    protected void SetRequirements(IReadOnlyList<Ingredient> requirements) {
        requirementUIContainer.ShowList(requirements);
        requirementUIContainer.gameObject.SetActive(requirements != null);
    }

    private void SetActivityNameText(string displayText) {
        activityNameText.text = displayText;
        activityNameText.gameObject.SetActive(!string.IsNullOrEmpty(displayText));
    }

    #region Progress

    protected override void OnProgressComplete() {
        var effects = contentSD.Effects;
        for (int i = 0; i < effects.Count; i++) {
            Effect effect = effects[i];
            Entity caster = Managers.Player.PlayerData.Entity;
            Managers.Effect.ApplyEffect(new EffectApplyRequest(effect, caster));
        }
    }

    #endregion


    #region Pool

    public override void Return() {
        base.Return();
        contentSD = null;
    }

    #endregion
}
