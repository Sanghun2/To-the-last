using System;
using UnityEngine;

public abstract class AnimatorBase : MonoBehaviour
{
    public abstract void Animate(
        Define.ActionAnimationType type, 
        Action onApplyTime=null,
        Action onComplete=null);
}
