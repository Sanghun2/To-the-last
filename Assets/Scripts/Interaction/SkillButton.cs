using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : ButtonBase
{
    private SkillData skillData;
    private List<StrategyBehaviour> strategyBehaviours;
    private BattleEntity caster;

    [Space]
    [Header("[  Assign  ]")]
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] Image skillIconImage;
    [SerializeField] GameObject selectedObj;

    public void InitSkill(BattleEntity caster, SkillData skillData) {
        if (caster == null) { Debug.LogError($"<color=red>[{GetType()}] skill 실행 주체가 없음. caster null</color>"); return; }
        this.caster = caster;

        if (skillData == null) { Debug.LogError($"<color=red>[{GetType()}] skill data null</color>"); return; }
        this.skillData = skillData;
        if (Managers.SD.TryGetSD(skillData.SkillID, out SkillSD skillSD)) {
            skillNameText.text = skillSD.DisplayName;
        }

        if (skillIconImage != null) {
            if (Managers.SD.TryGetSD(skillData.SkillSD.BehaviourType.ToID(), out ImageSD imageSD)) {
                skillIconImage.sprite = imageSD.IconImage;
            }
        }
        else {
            Debug.LogError($"<color=Orange>skill icon image null</color>");
        }

        for (int i = 0; i < skillSD.Effects.Count; i++) {
            var effect = skillSD.Effects[i];    
            strategyBehaviours.Add(new SkillBehaviour(
               caster,
               skillData.SkillSD.BehaviourType,
               (int)BattleUtility.CalculateBehaviourSpeed(caster)
               ));
        }

        caster.OnStateChanged -= UpdateState;
        caster.OnStateChanged += UpdateState;
    }


    protected override void ButtonAction() {
        if (caster == null) { Debug.LogError($"<color=red>[{GetType()}] caster null</color>"); return; }
        if (!CanAction()) return;

        caster.CurrentState = BattleEntity.BehaviourState.Selected;
        Managers.BattleSystem.RegisterBehaviour(strategyBehaviours);
    }


    private void UpdateState(BattleEntity.BehaviourState currentState, BattleEntity.BehaviourState prevState) {
        switch (currentState) {
            case BattleEntity.BehaviourState.Idle:
                selectedObj.SetActive(false);
                break;
            case BattleEntity.BehaviourState.Selected:
                selectedObj.SetActive(true);
                break;
            default:
                break;
        }
    }
    private bool CanAction() {
        return caster.CurrentState == BattleEntity.BehaviourState.Idle;
    }
}
