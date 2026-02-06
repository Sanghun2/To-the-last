using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 특정 일을 진행하게 되면 N초 동안 M의 시간을 흐르게 합니다
public class JobHandler : IInitializable
{
    public bool IsFocusJobRunning => currentFocusJob != null && currentFocusJob.Duration > 0 && currentFocusJob.TotalMinutes > 0;

    public bool IsInit => isInit;
    private bool isInit;

    private List<Job> jobList = new List<Job>(10);

    private FocusJob currentFocusJob = null;
    private Guid? currentFocusJobID = null;

    public JobHandler RegisterJob(Job job) {
        //if (IsFocusJobRunning) { Debug.LogError($"Job 충돌. 이미 focusJob 있음. 이런 상태가 발생한다면 queue방식으로 변경"); return this; }
        job.CurrentState = Job.State.Running;
        jobList.Add(job);
        Debug.Log($"{job} 등록됨");
        return this;
    }
    public void DoFocusJob(FocusJob focusJob, Action callback=null) {
        if (!IsFocusJobRunning) {
            currentFocusJob = focusJob;//jobQueue.Dequeue();
            focusJob.CurrentState = Job.State.Running;
            currentFocusJobID = Managers.Coroutine.StartCoroutine(FocusJobRoutine(focusJob, callback));
        }
    }

    private IEnumerator FocusJobRoutine(FocusJob focusJob, Action callback=null) {
        Managers.Time.PauseTime(true);
        float progress = 0;
        float currentTime = 0;

        float prevIngameSeconds = 0;
        float currentIngameSeconds = 0;

        Managers.Time.OnTimeChanged += focusJob.ChangeMinutes;

        while (progress < 1) {
            yield return null;
            // progress 계산
            currentTime += Time.deltaTime;
            progress = currentTime / focusJob.Duration;

            // ingame에서 몇 초 흐를지 계산
            prevIngameSeconds = currentIngameSeconds;
            currentIngameSeconds = Mathf.Lerp(0, focusJob.TotalSeconds, progress);
            float ingameDeltaSeconds = currentIngameSeconds - prevIngameSeconds;

            ChangeIngameTime(ingameDeltaSeconds);
        }

        Managers.Time.PauseTime(false);
        currentFocusJob = null;
        currentFocusJobID = null;
        Managers.Time.OnTimeChanged -= focusJob.ChangeMinutes;
    }
    private void ChangeIngameTime(float deltaSeconds) {
        Managers.Time.ChangeTime(deltaSeconds);
    }

    public void Init() {
        if (IsInit) return;
        Managers.Time.OnTimeChanged += ProcessJobList;
        isInit = true;
    }

    private void ProcessJobList(int day, int hour, int minute, int deltaMinute) {
        for (int i = 0; i < jobList.Count; i++) {
            var job = jobList[i];
            job.ChangeMinutes(deltaMinute);
            if (job.CurrentState == Job.State.Completed) {
                jobList.Remove(job);
            }
        }
    }

    public void Release() {

    }
}
