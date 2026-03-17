using System;
using BilliotGames;
using UnityEngine;

public interface IAnimatableState
{
    public Define.ActionAnimationType AnimationType { get; }
    public Action OnApplyTime { get; }
    public Action OnComplete { get; }
}

public class AttackState : StateBase, IAnimatableState
{
    public Define.ActionAnimationType AnimationType => animationType;
    public Action OnApplyTime => onApplyTime;
    public Action OnComplete => onComplete;

    private Define.ActionAnimationType animationType;
    private Action onApplyTime;
    private Action onComplete;

    public AttackState(Define.ActionAnimationType animationType, Action onApplyTime=null, Action onComplete=null) {
        this.animationType = animationType;
        this.onApplyTime = onApplyTime;
        this.onComplete = onComplete;
    }


    public override void EnterState() {

    }

    public override void ExitState() {

    }

    public override void UpdateState() {

    }
}
