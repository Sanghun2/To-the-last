using System;
using BilliotGames;
using UnityEngine;

public interface IAnimationPlayer
{
    public void Animate(Action onComplete = null);
}

public abstract class AnimatedUI : UIBase, IAnimationPlayer
{
    public abstract void Animate(Action onComplete = null);
}
