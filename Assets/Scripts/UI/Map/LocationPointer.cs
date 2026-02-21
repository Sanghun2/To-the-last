using System;
using System.Collections;
using BilliotGames;
using UnityEngine;

public class LocationPointer : UIBase
{
    public RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();
            }

            return _rect;
        }
    }
    public bool Pause => pause;

    protected RectTransform _rect;
    private Guid currentRoutineID;
    private bool pause;

    public void MovePosition(Vector2 startPos, Vector2 endPos, float duration, Action callback=null) {
        StopMove(currentRoutineID);
        currentRoutineID = Managers.Coroutine.StartCoroutine(MoveRoutine(startPos, endPos, duration, callback));
    }
    internal void PauseMove(bool pause) {
        this.pause = pause;
        Managers.Job.PauseJob(pause);
    }

    IEnumerator MoveRoutine(Vector2 startPos, Vector2 endPos, float duration, Action callback=null) {        
        Vector2 currentPos = startPos;
        float percent = 0;
        float currentTime = 0;

        //FocusJob fj = new FocusJob(Vector2.Distance(startPos, endPos));
        //Managers.Job.DoFocusJob(fj);


        while (percent < 1) {
            while (pause) { yield return null; }
            percent = currentTime / duration;
            Rect.anchoredPosition = Vector2.Lerp(startPos, endPos, percent);
            yield return null;
            currentTime += Time.deltaTime;
            if (currentTime > duration) {
                currentTime = duration;
            }
        }

        callback?.Invoke();
    }
    private void StopMove(Guid currentRoutineID) {
        if (currentRoutineID != default) {
            Managers.Coroutine.StopCoroutine(currentRoutineID);
        }
    }
}
