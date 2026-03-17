using System;
using DG.Tweening;
using UnityEngine;

public class SkillContentUI : AnimatedUI
{
    private RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();
            }

            return _rect;
        }
    }

    [SerializeField] float animationDelay = 0;
    [SerializeField] float animationDuration = 1f;
    [SerializeField] Ease ease;
    private RectTransform _rect;
    private Tween tween;

    public void SetOptions(float delay, float duration) {
        animationDelay = delay;
        animationDuration = duration;
    }
    public override void Animate(Action onComplete = null) {
        tween?.Kill();
        ResetPosition();

        tween = Rect.DOAnchorPosX(0, animationDuration)
            .SetEase(ease)
            .SetDelay(animationDelay)
            .OnComplete(() => {
                onComplete?.Invoke();
                tween = null;
            });
    }

    private void OnDestroy() {
        tween?.Kill();
    }
    private void ResetPosition() {
        Rect.anchoredPosition = new Vector2(Rect.rect.width, 0);
    }
}
