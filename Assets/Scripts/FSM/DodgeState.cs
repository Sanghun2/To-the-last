using System;
using BilliotGames;
using UnityEngine;

public class DodgeState : StateBase, IAnimatableState
{
    public Define.ActionAnimationType AnimationType => Define.ActionAnimationType.Dodge;

    public Action OnApplyTime => onApplyTime;
    public Action OnComplete => onComplete;


    private Action onApplyTime;
    private Action onComplete;
    
    public DodgeState(Action onApplyTime=null, Action onComplete=null) {
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
