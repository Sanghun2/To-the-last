using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : ButtonBase, IPool
{
    private SkillData skillData;
    private List<StrategyBehaviour> strategyBehaviours;
    private BattleEntity caster;

    [Space]
    [Header("[  Assign  ]")]
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] Image skillIconImage;
    [SerializeField] GameObject selectedObj;

    public bool IsActive => skillData != null && IsOpened;

    public void InitSkill(SkillData skillData) {
        BattleEntity caster = Managers.BattleSystem.GetPlayerEntity();
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

        strategyBehaviours = new List<StrategyBehaviour>(skillSD.Effects.Count);
        for (int i = 0; i < skillSD.Effects.Count; i++) {
            var effect = skillSD.Effects[i];
            strategyBehaviours.Add(new SkillBehaviour(
               caster,
               skillData.SkillSD.BehaviourType,
               (int)BattleUtility.CalculateBehaviourSpeed(caster),
               Managers.BattleSystem.GetFirstEnemy()
               ));
        }

        caster.OnStateChanged -= UpdateState;
        caster.OnStateChanged += UpdateState;

        UpdateState(BattleEntity.BehaviourState.Idle, BattleEntity.BehaviourState.Idle);
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

    #region Pool

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}
