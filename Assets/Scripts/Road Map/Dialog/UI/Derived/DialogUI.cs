using System;
using System.Collections;
using UnityEngine;

public class DialogUI : DialogUIBase
{
    private Guid routineID;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public override void SkipDialogAnimation() {
        if (CurrentPageState != PageState.InProgress) return;
        if (routineID != default) Managers.Coroutine.StopCoroutine(routineID);
        dialogText.maxVisibleCharacters = dialogText.text.Length;
        CurrentPageState = PageState.Completed;
    }

    protected override void AnimatePage() {
        if (CurrentPageState == PageState.Completed) return;
        if (routineID != default) Managers.Coroutine.StopCoroutine(routineID);

        routineID = Managers.Coroutine.StartCoroutine(PageAnimationRoutine());
    }

    private IEnumerator PageAnimationRoutine(Action onCompleted=null) {
        dialogText.maxVisibleCharacters = 0;
        int maxCharacters = dialogText.text.Length;
        int viewedCharacters = 0;

        var waitSec = new WaitForSeconds(0.5f);

        while (viewedCharacters < maxCharacters) {
            yield return waitSec;
            viewedCharacters++;
            dialogText.maxVisibleCharacters = viewedCharacters;
        }

        routineID = default;
        CurrentPageState = PageState.Completed;
        onCompleted?.Invoke();
    }
}