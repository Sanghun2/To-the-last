using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : ButtonBase, IPool
{
    private SkillData skillData;
    private SkillBehaviour skillBehaviour;
    private BattleEntity caster;

    [Space]
    [Header("[  Assign  ]")]
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] Image skillIconImage;
    [SerializeField] GameObject selectedObj;

    public bool IsActive => skillData != null && IsOpened;

    public void InitSkill(SkillData skillData) {
        if (IsInit) return;
        this.caster = Managers.BattleSystem.GetPlayerEntity();
        if (caster == null) { Debug.LogError($"<color=red>[{GetType()}] skill 실행 주체가 없음. caster null</color>"); return; }
    

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

        skillBehaviour = new SkillBehaviour(caster, skillSD);

        caster.OnStateChanged -= UpdateState;
        caster.OnStateChanged += UpdateState;

        UpdateState(BattleEntity.BehaviourState.Idle, BattleEntity.BehaviourState.Idle);

        _isInit = true;
    }


    protected override void ButtonAction() {
        if (caster == null) { Debug.LogError($"<color=red>[{GetType()}] caster null</color>"); return; }
        if (!CanAction()) return;

        caster.CurrentState = BattleEntity.BehaviourState.Selected;
        Managers.BattleSystem.RegisterBehaviour(skillBehaviour);
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

        base._isInit = true;
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}
