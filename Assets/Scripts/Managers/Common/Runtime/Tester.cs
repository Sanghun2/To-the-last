using System;
using System.Collections.Generic;
using BilliotGames;
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

    [Space]
    [Header("[  Inventory Test  ]")]
    [SerializeField] ItemSD itemSD;
    [SerializeField] int amount;

    public void PushItem() {
        Managers.Player.Inventory.TryPushItem(new ItemStack(itemSD.ToItemData(), amount), out var overflowedStack);
    }
    public void PopItem() {
        Managers.Player.Inventory.TryRemoveItem(itemSD.ID, amount);
    }

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

    public void UnlockStructureUI() {
        Managers.Construction.Unlock(locationIndex);
    }
    public void SetStructure() {
        Managers.Construction.Construct(locationIndex, targetStructureSD);
    }
    public void Destroy() {
        Managers.Construction.Destroy(locationIndex);
    }

    public void ShowBuildList() {
        Managers.UI.GetUI<ConstructionUI>().ShowConstructionList(testStructureSDList);
    }
}
