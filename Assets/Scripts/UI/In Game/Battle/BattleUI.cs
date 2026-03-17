using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] BattleEntityUI playerUI;
    [SerializeField] BattleEntityUI enemyUI;
    [SerializeField] TurnUI _turnUI;
    [SerializeField] FloatingTextContainer floatingTextContainer;
    [SerializeField] List<SkillButton> skillButtonList;
    [SerializeField] UIAnimationController uiAnimationController;
    private Action onAnimationCompleted;

    public override void InitUI() {
        if (IsInit) return;

        uiAnimationController?.InitUI();
        TurnUI.InitUI();
        CloseUI();
        uiAnimationController.OnAnimationCompleted -= OnAnimationComplete;
        uiAnimationController.OnAnimationCompleted += OnAnimationComplete;

        _isInit = true;
    }

    internal void InitUI(BattleEntity player, BattleEntity enemy) {
        playerUI.InitEntity(player, null);
        enemyUI.InitEntity(enemy, player);
    }
    internal void InitSkillUI(IReadOnlyList<SkillData> skillList) {
        if (skillList == null) { Debug.LogError($"<color=red>skill list null</color>"); return; }
        if (skillButtonList == null) { Debug.LogError($"<color=red>skill button null</color>"); return; }
        var validSkills = skillList.Where(sk => !string.IsNullOrEmpty(sk.SkillID)).ToList();


        ClearSkillButtons();
        for (int i = 0; i < validSkills.Count; i++) {
            SkillData skillData = validSkills[validSkills.Count-i-1];
            //if (string.IsNullOrEmpty(skillData.SkillID)) continue;
            var skillButton = skillButtonList[i];
            skillButton.InitSkill(skillData);
            float delay = (validSkills.Count-i-1) * 0.15f;
            float duration = 0.75f;
            skillButton.SkillContentUI.SetOptions(delay, duration);
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
    internal void ShowAnimation(Action onComplete=null) {
        onAnimationCompleted = onComplete;
        uiAnimationController.AnimateUIs();
    }


    private void ClearSkillButtons() {
        for (int i = 0; i < skillButtonList.Count; i++) {
            skillButtonList[i].CloseUI();
        }
    }

    private void OnAnimationComplete() {
        onAnimationCompleted?.Invoke();
        Debug.Log($"battle ui animation complete");
    }
}
