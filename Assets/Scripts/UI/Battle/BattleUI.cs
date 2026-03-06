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
    [SerializeField] SkillButtonContainer skillButtonContainer;

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
        if (skillButtonContainer == null) { Debug.LogError($"<color=red>skill button null</color>"); return; }

        skillButtonContainer.Clear();
        for (int i = 0; i < skillList.Count; i++) {
            SkillData skillData = skillList[i];
            var skillButton = skillButtonContainer.GetObj(i);
            skillButton.InitSkill(skillData);
        }
    }
}
