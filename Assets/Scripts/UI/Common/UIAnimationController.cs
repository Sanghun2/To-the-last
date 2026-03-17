using System;
using BilliotGames;
using UnityEngine;

public class UIAnimationController : UIBase
{
    [SerializeField] AnimatedUI[] animatedUIs;
    private int completeCount;

    public event Action OnAnimationCompleted;

    public override void InitUI() {
        if (IsInit) return;

        FindUIs();
        for (int i = 0; i < animatedUIs.Length; i++) {
            var animatedUI = animatedUIs[i];
            animatedUI.InitUI();
        }

        _isInit = true;
    }

    public void AnimateUIs() {
        completeCount = 0;

        for (int i = 0; i < animatedUIs.Length; i++) {
            var animatedUI = animatedUIs[i];
            animatedUI.Animate(RaiseCompleteCount);
        }
    }

    private void FindUIs() {
        animatedUIs = GetComponentsInChildren<AnimatedUI>(includeInactive:true);
    }
    private void RaiseCompleteCount() {
        completeCount++;
        if (completeCount == animatedUIs.Length) {
            OnAnimationCompleted?.Invoke();
        }
    }
}
