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

    [SerializeField] Image backgroundImage;
    [SerializeField] EntityUI playerUI;
    [SerializeField] EntityUI enemyUI;
    [SerializeField] TurnUI _turnUI;
    [SerializeField] List<SkillButton> skillButtonList;

    public override void InitUI() {
        if (IsInit) return;

        TurnUI.InitUI();

        _isInit = true;
    }

    internal void InitUI(Entity player, Entity enemy) {
        playerUI.InitEntity(player);
        enemyUI.InitEntity(enemy);
    }

    internal void InitSkillUI(IReadOnlyList<SkillData> skillList) {
        if (skillList == null) { Debug.LogError($"<color=red>skill list null</color>"); return; }
        if (skillButtonList == null) { Debug.LogError($"<color=red>skill button null</color>"); return; }

        ClearSkillButtons();
        for (int i = 0; i < skillList.Count; i++) {
            SkillData skillData = skillList[i];
            var skillButton = skillButtonList[i];
            skillButton.InitSkill(skillData);
            skillButton.OpenUI();
        }
    }

    private void ClearSkillButtons() {
        for (int i = 0; i < skillButtonList.Count; i++) {
            skillButtonList[i].CloseUI();
        }
    }
}
