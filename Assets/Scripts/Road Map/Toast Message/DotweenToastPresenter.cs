using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DotweenToastPresenter : ToastPresenterBase
{
    [SerializeField] float slideDuration = 0.75f;
    [SerializeField] float fadeDuration = 0.4f;
    private Ease slideInEase;
    private Ease fadeOutEase;

    public DotweenToastPresenter(Ease slideInEase, Ease fadeOutEase, float slideDuration=0.75f, float fadeDuration=0.4f) {
        this.slideInEase = slideInEase;
        this.slideDuration = slideDuration;
        this.fadeOutEase = fadeOutEase;
        this.fadeDuration = fadeDuration;
    }

    public override void PresentToast(ToastMessageUI toastMessageUI, Vector2 endPos, float viewDuration) {
        PresentAsync(toastMessageUI, endPos, viewDuration).Forget();
    }

    private async UniTaskVoid PresentAsync(ToastMessageUI toastMessageUI, Vector2 endPos, float viewDuration) {
        var rect = toastMessageUI.Rect;
        var canvasGroup = toastMessageUI.CanvasGroup;

        // 초기 상태
        canvasGroup.alpha = 1f;

        // 슬라이드 인
        await rect.DOAnchorPos(endPos, slideDuration)
                  .SetEase(slideInEase)
                  .AsyncWaitForCompletion();

        // 대기
        await UniTask.WaitForSeconds(viewDuration);

        // 페이드 아웃
        await canvasGroup.DOFade(0f, fadeDuration)
                         .SetEase(fadeOutEase)
                         .AsyncWaitForCompletion();

        toastMessageUI.Return();
    }
}