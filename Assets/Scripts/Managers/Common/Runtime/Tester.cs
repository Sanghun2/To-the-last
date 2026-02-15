using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [Header("[  Stat Test  ]")]
    [SerializeField] Define.Stat targetStat;
    [SerializeField] float deltaValue = 5;

    [Space]
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

    [Space]
    [Header("[  Map Test  ]")]
    [SerializeField] LocationSD locationSD;

    public void ShowLocationPopUp() {
        Managers.UI.OpenUI<LocationInfoPopUpUI>().InitPopUp(new LocationInfoPopUpData(
            locationSD,
            new ActionData[] {
                new ActionData("확인", () => Managers.UI.CloseTopUI()),
                new ActionData("진입", null)
            }));
    }

    public void ChangeValue() {
        Managers.Player.Player.ChangeStat(targetStat, deltaValue);
    }

    public void PushItem() {
        Managers.Player.Inventory.TryPushItem(new ItemStack(itemSD.ToItemData(), amount), out var overflowedStack);
    }
    public void PopItem() {
        Managers.Player.Inventory.TryRemoveItem(itemSD.ID, amount);
    }
    public void ShowInventory() {
        Managers.UI.OpenUI<InventoryUI>().ShowInventory(Managers.Player.Inventory);
    }

    public void DoTask() {
        if (testFocusJob != null) {
            var craftUI = Managers.UI.GetUI<CraftStructureUI>();
            craftUI.InitProgressUI(0, 1);
            var fJob = new FocusJob(testFocusJob.TotalMinutes, testFocusJob.Duration, (current, total) => {
                craftUI.UpdateProgressUI(current, total);
            });
            Managers.Job.DoFocusJob(fJob);
        }
    }
    public void RegisterTask() {
        if (testJob != null) {
            Managers.Job.RegisterDelayedJob(testJob);
        }
    }

    public void UnlockStructureUI() {
        Managers.Construction.Unlock(locationIndex);
    }
    public void SetStructure() {
        Managers.Construction.PlaceStructure(locationIndex, targetStructureSD);
    }
    public void Destroy() {
        Managers.Construction.Destroy(locationIndex);
    }

    public void ShowBuildList() {
        Managers.UI.OpenUI<ConstructionUI>().ShowConstructionCatalogs(testStructureSDList);
    }
}
