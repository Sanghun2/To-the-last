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
    public bool IsMoving => isMoving;

    protected RectTransform _rect;
    private Guid currentRoutineID;
    private bool pause;
    private bool isMoving;
    private const float MOVE_DURATION = 2f;

    internal void SetPosiion(string locationID) {
        if (Managers.SD.TryGetSD(locationID, out LocationSD targetSD)) {
            var pos = targetSD.AnchoredPosition;
            Rect.anchoredPosition = pos;
        }
        else {
            Debug.LogError($"faile to find ({locationID}) location SD");
        }
    }

    public void MovePosition(Vector2 startPos, Vector2 endPos, float duration = MOVE_DURATION, Action callback = null) {
        StopMove(currentRoutineID);
        currentRoutineID = Managers.Coroutine.StartCoroutine(MoveRoutine(startPos, endPos, duration, callback));
    }
    public void MovePosition(LocationSD startLocationSD, LocationSD endLocationSD, float duration = MOVE_DURATION, Action callback = null) {
        MovePosition(startLocationSD.AnchoredPosition, endLocationSD.AnchoredPosition, duration, callback);
    }
    public void MovePosition(Location startLocation, Location endLocation, float duration = MOVE_DURATION, Action callback = null) {
        MovePosition(startLocation.LocationSD, endLocation.LocationSD, duration, callback);
    }
    internal void PauseMove(bool pause) {
        this.pause = pause;
        Managers.Job.PauseJob(pause);
    }

    IEnumerator MoveRoutine(Vector2 startPos, Vector2 endPos, float duration, Action callback=null) {
        isMoving = true;
        Vector2 currentPos = startPos;
        float percent = 0;
        float currentTime = 0;

        FocusJob moveJob = new FocusJob(
            LocationUtility.CalculateDistance(startPos, endPos).ConvertToMinutes(),
            duration);
        Managers.Job.DoFocusJob(moveJob);


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
        isMoving = false;
    }
    private void StopMove(Guid currentRoutineID) {
        if (currentRoutineID != default) {
            Managers.Coroutine.StopCoroutine(currentRoutineID);
            isMoving = false;
        }
    }
}
