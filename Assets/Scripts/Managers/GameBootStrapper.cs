using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBootStrapper
{
    public event Action OnGameStarted;

    public void StartBootStrap() {
        // 연출

        // 체인 시작
        Managers.Process.TryStartProcess(Define.FlowType.BootStrapGame);
    }

    public void CancelBootStrap() {
        Managers.UI.CloseUI<GameBootStrapUI>();
        Managers.Trait.ResetTraits();
        Managers.Character.ResetCharacters();
    }

    public void CompleteBootStrap() {
        Managers.UI.CloseUI<GameBootStrapUI>();

        Managers.Scene.TransitionScene(Define.INGAME_SCENE_ID, callback: () => {
            InitAllGameData();
            OnGameStarted?.Invoke();
        });
    }

    private static void InitAllGameData() {

        // time
        Managers.Time.SetAsDefaultTime();
        Managers.Time.PauseMainTime(false);

        // stats
        var playerData = Managers.Player.PlayerData;
        if (!Managers.SD.TryGetSD<CharacterSD>(playerData.CharacterID, out var characterSD)) return;

        playerData.MetabolicSystem.InitMetabolism(BuildMetabolism());
        playerData.StatContainer.InitStats(characterSD.StatList);


        // location
        //int basementMarkerIndex = 5;
        //LocationUtility.MarkerGridGenerator.InitGrid(basementMarkerIndex);
        Managers.Player.PlayerData.SetAsDefaultLocation();
        //Managers.Location.OnLocationChanged -= playerData.SetCurrentLocation;
        //Managers.Location.OnLocationChanged += playerData.SetCurrentLocation;

        // structure
        Managers.Structure.UnlockLocations(targetExpensionLevel:0);

        // inventory
        var playerInventory = new SimpleInventory("player").SetTag("player");
        Managers.Inventory.AddInventory(playerInventory);
        var counter = new WeightCounter(50);
    }

    private static IReadOnlyList<(Define.Stat, float)> BuildMetabolism() {
        var characterID = Managers.Player.PlayerData.CharacterID;
        if (Managers.SD.TryGetSD<CharacterSD>(characterID, out var characterSD)) {
            return characterSD.MetabolismDatas.Select(data => (data.TargetStat, data.ConsumeAmount)).ToList();
        }

        Debug.LogError($"<color=red>({characterID}) character SD not exist</color>");        
        return null;
    }
}
