using System;
using BilliotGames;
using UnityEngine;

public class IdleState : StateBase, IAnimatableState
{
    public Define.ActionAnimationType AnimationType => animationType;

    public Action OnApplyTime => null;
    public Action OnComplete => null;

    public IdleState() {
        animationType = Define.ActionAnimationType.Default;
    }

    private Define.ActionAnimationType animationType;

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override void UpdateState() {
    }
}
