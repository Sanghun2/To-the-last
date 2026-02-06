using System;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] Job testJob;
    [SerializeField] FocusJob testFocusJob;

    public void DoTask() {
        if (testFocusJob != null) {
            var craftUI = Managers.UI.GetUI<CraftUI>();
            craftUI.InitProgressUI(0, 1);
            var fJob = new FocusJob(testFocusJob.TotalMinutes, testFocusJob.Duration, (current, total) => {
                craftUI.UpdateProgressUI(current, total);
            });
            Managers.Job.DoFocusJob(fJob);
        }
    }
    public void RegisterTask() {
        if (testJob != null) {
            Managers.Job.RegisterJob(testJob);
        }
    }
}
