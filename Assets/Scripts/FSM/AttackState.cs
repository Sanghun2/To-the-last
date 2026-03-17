using System;
using BilliotGames;
using UnityEngine;

public interface IAnimatableState
{
    public Define.ActionAnimationType AnimationType { get; }
    public Action OnApplyTime { get; }
}

public class AttackState : StateBase, IAnimatableState
{
    public Define.ActionAnimationType AnimationType => animationType;
    public Action OnApplyTime => onApplyTime;

    private Define.ActionAnimationType animationType;
    private Action onApplyTime;

    public AttackState(Define.ActionAnimationType animationType, Action onApplyTime=null) {
        this.animationType = animationType;
        this.onApplyTime = onApplyTime;
    }


    public override void EnterState() {

    }

    public override void ExitState() {

    }

    public override void UpdateState() {

    }
}
