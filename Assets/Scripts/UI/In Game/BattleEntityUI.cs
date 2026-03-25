using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class BattleEntityUI : EntityUI
{
    [SerializeField] GaugeUI hpUI;
    [SerializeField] TextMeshProUGUI distanceText;
    private BattleEntity self;
    private BattleEntity target;

    [Obsolete("this is invalid init way. use InitEntity(BattleEntity self, BattleEntity target)", error:true)]
    public override void InitEntity(Entity entity) {
        //if (entity is BattleEntity battleEntity) {
        //    InitEntity(battleEntity, null);
        //}
        //else {
        //    Debug.LogError($"<color=red>({entity.GetType()}) is not battle entity</color>");
        //}

        Debug.LogError($"this is invalid init way. use InitEntity(BattleEntity self, BattleEntity target)");
    }

    public void InitEntity(BattleEntity self, BattleEntity target) {
        base.InitEntity(self);
        InitHpStatUI(self);
        this.self = self;
        this.target = target;

        self.OnDistanceChanged -= RefreshDistanceUI;
        self.OnDistanceChanged += RefreshDistanceUI;
        self.OnStateChanged -= UpdateState;
        self.OnStateChanged += UpdateState;

        if (target != null) {
            target.OnDistanceChanged -= RefreshDistanceUI;
            target.OnDistanceChanged += RefreshDistanceUI;
            RefreshDistanceUI();
        }
    }

    private void UpdateState(StateBase currentState, StateBase prevState) {
        if (currentState is IAnimatableState state) {
            animtor.Animate(state.AnimationType, state.OnApplyTime, state.OnComplete);
        }
    }

    private void RefreshDistanceUI(int position) {
        RefreshDistanceUI();
    }

    private void RefreshDistanceUI() {
        if (self == null || target == null) { Debug.LogError($"<color=red>계산 대상이 없음</color>"); return; }

        if (distanceText != null) {
            distanceText.text = self.CalculateDistance(target).ToString();
        }
    }

    private void InitHpStatUI(BattleEntity battleEntity) {
        if (hpUI != null) {
            if (!battleEntity.TryGetStat(Define.Stat.Hp.ToID(), out IStatEntry hpStat)) { Debug.LogError($"<color=red>hp stat is not exist</color>"); return; }

            if (hpStat is BoundedStat boundedHpStat) {
                boundedHpStat.OnValueChanged -= hpUI.UpdateUI;
                boundedHpStat.OnValueChanged += hpUI.UpdateUI;
                var value = hpStat.RawValue;
                hpUI.UpdateUI(value.CurrentValue, value.MaxValue);
            }
            else {
                Debug.LogError($"<color=red>hp stat is not bounded stat</color>");
            }
        }
    }
}
