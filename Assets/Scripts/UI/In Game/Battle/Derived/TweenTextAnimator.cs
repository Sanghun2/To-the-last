using System;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

public class TweenTextAnimator : TextAnimatorBase
{
    [Header("[  Fade  ]")]
    [SerializeField] float fadeDelay = 1f;
    [SerializeField] float fadeOutDuration = 1f;
    [SerializeField] Ease fadeOutEase = Ease.OutFlash;

    [Space]
    [Header("[  Move  ]")]
    [SerializeField] float floatDistance = 50;
    [SerializeField] Ease floatEase = Ease.InFlash;

    private Tween tween;


    public override void StopAnimating() {
        tween?.Kill();
        CurrentState = ITextAnimator.State.Idle;
    }

    public override void AnimateText(AnimationTarget target, Action callback = null) {
        if (CurrentState == ITextAnimator.State.Animating) return;

        CurrentState = ITextAnimator.State.Animating;

        var group = target.group;
        group.alpha = 1;
        var rect = target.rect;

        Vector2 startPos = rect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, floatDistance); // 올라갈 거리

        tween = DOTween.Sequence()
            .Append(group.DOFade(0, fadeOutDuration).SetEase(fadeOutEase).SetDelay(fadeDelay))
            .Join(rect.DOAnchorPos(endPos, fadeOutDuration).SetEase(floatEase))
            .OnComplete(() => {
                rect.anchoredPosition = startPos; // 위치 초기화
                CurrentState = ITextAnimator.State.Idle;
                callback?.Invoke();
            });
    }
}
