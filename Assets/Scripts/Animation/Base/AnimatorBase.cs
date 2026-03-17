using System;
using UnityEngine;

public abstract class AnimatorBase : MonoBehaviour
{
    public abstract void Animate(
        Define.ActionAnimationType type, 
        Action onApplyTime=null,
        Action onComplete=null);


    protected string ConvertTypeToParameter(Define.ActionAnimationType type) {

        switch (type) {
            case Define.ActionAnimationType.Default:
                return "Idle";
            case Define.ActionAnimationType.SwingAttack:
                return "Swing";
            case Define.ActionAnimationType.StabAttack:
                return "Stab";
            case Define.ActionAnimationType.Dodge:
                return "Dodge";
            default:
                Debug.Log($"<color=orange>not defined type ({type})</color>");
                return "Idle";
        }
    }
}
