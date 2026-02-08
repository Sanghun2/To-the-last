using System;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [Header("[  Job Handler Test  ]")]
    [SerializeField] Job testJob;
    [SerializeField] FocusJob testFocusJob;

    [Space]
    [Header("[  Build UI Test  ]")]
    [SerializeField] List<StructureSD> testStructureSDList;
    [Space]
    [SerializeField] int locationIndex;
    [SerializeField] StructureSD targetStructureSD;

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

    public void SetStructure() {
        Managers.Construction.Construct(locationIndex, targetStructureSD);
    }

    public void ShowBuildList() {
        Managers.UI.GetUI<BuildingUI>().ShowConstructionList(testStructureSDList);
    }
}
