using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface ITextAnimator
{
    public enum State
    {
        Idle,
        Animating,
    }

    public State CurrentState { get; set; }
    public abstract void AnimateText(AnimationTarget target, Action callback = null);
    public abstract void StopAnimating();
}

public struct AnimationTarget
{
    public CanvasGroup group;      // 페이드용
    public TextMeshProUGUI text;   // 텍스트 변경용
    public Image icon;             // 아이콘용 (nullable)
    public RectTransform rect;
}

public abstract class TextAnimatorBase : MonoBehaviour, ITextAnimator
{
    public ITextAnimator.State CurrentState
    {
        get => currentState;
        set
        {
            //var prevState = _currentProgress;
            currentState = value;
            //if (prevState != _currentProgress) {
            //    UpdateState
            //}
        }
    }

    private ITextAnimator.State currentState;

    public abstract void AnimateText(AnimationTarget target, Action callback = null);
    public abstract void StopAnimating();
}
