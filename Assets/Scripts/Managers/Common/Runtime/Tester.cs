using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] int progress;

    [Space] 
    [SerializeField] EncounterSD encounterSD;
    [SerializeField] CharacterSD characterSD;
    [SerializeField] string situation;
    [SerializeField] string[] descriptions;

    [Space]
    [SerializeField] LocationSD startLocationSD;
    [SerializeField] LocationSD endLocationSD;
    [SerializeField] float moveDuration = 2f;

    [Space]
    [SerializeField] LootSelectionSD lootSelectionSD;
    [SerializeField] SkillSD[] testSkills;
    [SerializeField] EnemySD enemySD;
    [SerializeField] BattleEntity enemy;

    public void PrepareBattles() {

        var skills = testSkills.Select(s => new SkillData(s)).ToArray();
        for (int i = 0; i < skills.Length; i++) {
            if (i >= 4) break;
            SkillData skill = skills[i];
            Managers.Player.PlayerData.RegisterSkill(i, skill);
        }

        enemy = new BattleEntity("zombie").InitEntity(enemySD.StatDataList);
        Managers.BattleSystem.PrepareBattle(
            new BattleEntity("jadon").InitEntity(Managers.Player.PlayerData.StatContainer), 
            enemy,
            () => {
                Managers.BattleSystem.StartBattle();
            });
    }

    public void MovePointer() {
        var start = startLocationSD.AnchoredPosition;
        var end = endLocationSD.AnchoredPosition;
        Managers.Job.StopFocusJob();
        Managers.UI.GetUI<LocationPointer>().MovePosition(start, end, moveDuration, () => {
            Debug.Log($"이동 완료");
        });
    }
    public void PausePointer() {
        var pointer = Managers.UI.GetUI<LocationPointer>();
        pointer.PauseMove(!pointer.Pause);
    }

    public void ShowSelections() {
        Debug.Log($"this is obsolete function");
        //List<SelectActionData> list = new List<SelectActionData>();
        //for (int i = 0; i < descriptions.Length; i++) {
        //    int idx = i;
        //    list.Add(new SelectActionData(descriptions[idx], () => Debug.Log($"{descriptions[idx]} selected")));
        //}
        //var dd = new DialogData(characterSD.StructureImage, characterSD.DisplayText, situation, list);
        //Managers.UI.OpenStructureUI<DialogUI>().ShowDialog(dd);
    }
    public void ExecuteEncounter() {

    }

    public void ActivateLocation() {
        Managers.Location.UnlockLocation(locationSD.ID, progress);
    }
    public void DeactivateLocation() {
        Managers.Location.DeactivateLocation(locationSD);
    }
    public void ShowLocationPopUp() {
        if (Managers.Location.TryGetLocation(locationSD, out Location location)) {
            Managers.UI.OpenUI<LocationInfoPopUpUI>().InitPopUp(new LocationInfoPopUpData(
           location,
           new ActionData[] {
                new ActionData("확인", () => Managers.UI.CloseTopUI()),
                new ActionData("진입", null)
           }));
        }
    }

    public void ChangeValue() {
        Managers.Player.PlayerData.ChangeStat(targetStat, deltaValue);
    }

    public void PushItem() {
        Managers.Player.Inventory.TryPushItem(new ItemStack(itemSD.ToData(), amount), out var overflowedStack);
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
            var fJob = new FocusJob(testFocusJob.TotalMinutes, testFocusJob.Duration, onProgress:(current, total) => {
                craftUI.UpdateProgressUI(current, total);
            }).WithBlockScreen();
            Managers.Job.DoFocusJob(fJob);
        }
    }
    public void RegisterTask() {
        if (testJob != null) {
            Managers.Job.RegisterDelayedJob(testJob);
        }
    }

    public void UnlockStructureUI() {
        Managers.Construction.UnlockLocation(locationIndex);
    }
    public void SetStructure() {
        //Managers.Construction.StartConstruction(locationIndex, currentStructureData);
    }
    public void Destroy() {
        Managers.Construction.DestroyStructureAt(locationIndex);
    }

    public void ShowBuildList() {
        Managers.UI.OpenUI<ConstructionUI>().ShowConstructionCatalogs(testStructureSDList);
    }
}
