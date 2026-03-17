using System;
using BilliotGames;
using UnityEngine;

public class ChargeState : StateBase, IAnimatableState
{
    public Define.ActionAnimationType AnimationType => Define.ActionAnimationType.Charge;
    public Action OnApplyTime => null;
    public Action OnComplete => onComplete;

    private Action onComplete;

    public ChargeState(Action onComplete = null) {
        this.onComplete = onComplete;
    }

    public override void EnterState() {

    }

    public override void ExitState() {

    }

    public override void UpdateState() {

    }
}
