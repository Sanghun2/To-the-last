using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIBase
{
    public TurnUI TurnUI
    {
        get
        {
            if (_turnUI == null) {
                _turnUI = FindAnyObjectByType<TurnUI>(FindObjectsInactive.Include);
            }

            return _turnUI;
        }
    }
    public FloatingTextContainer FloatingText
    {
        get
        {
            if (floatingTextContainer == null) {
                floatingTextContainer = GetComponentInChildren<FloatingTextContainer>();
                if (floatingTextContainer == null) { Debug.LogError($"<color=red>floating text container could not find</color>"); }
                floatingTextContainer?.InitUI();
            }

            return floatingTextContainer;
        }
    }

    [SerializeField] Image backgroundImage;
    [SerializeField] EntityUI playerUI;
    [SerializeField] BattleEntityUI enemyUI;
    [SerializeField] TurnUI _turnUI;
    [SerializeField] FloatingTextContainer floatingTextContainer;
    [SerializeField] List<SkillButton> skillButtonList;

    public override void InitUI() {
        if (IsInit) return;

        TurnUI.InitUI();
        CloseUI();

        _isInit = true;
    }
    internal void InitUI(BattleEntity player, BattleEntity enemy) {
        playerUI.InitEntity(player);
        enemyUI.InitEntity(enemy, player);
    }
    internal void InitSkillUI(IReadOnlyList<SkillData> skillList) {
        if (skillList == null) { Debug.LogError($"<color=red>skill list null</color>"); return; }
        if (skillButtonList == null) { Debug.LogError($"<color=red>skill button null</color>"); return; }

        ClearSkillButtons();
        for (int i = 0; i < skillList.Count; i++) {
            SkillData skillData = skillList[i];
            if (string.IsNullOrEmpty(skillData.SkillID)) continue;
            var skillButton = skillButtonList[i];
            skillButton.InitSkill(skillData);
            skillButton.OpenUI();
        }
    }


    internal EntityUI GetEntityUI(BattleEntity targetEntity) {
        if (playerUI.Entity.EntityID.Equals(targetEntity.EntityID)) {
            return playerUI;
        }
        else {
            return enemyUI;
        }
    }


    private void ClearSkillButtons() {
        for (int i = 0; i < skillButtonList.Count; i++) {
            skillButtonList[i].CloseUI();
        }
    }
}
