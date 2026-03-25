using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBootStrapper
{
    public void StartBootStrap() {
        // 연출

        // 체인 시작
        Managers.UI.OpenUI<GameBootStrapUI>();
        Managers.Process.StartProcess(Define.FlowType.BootStrapGame);
    }

    public void CancelBootStrap() {
        Managers.UI.CloseUI<GameBootStrapUI>();
        Managers.Trait.ResetTraits();
        Managers.Character.ResetCharacters();
    }

    public void CompleteBootStrap() {
        Debug.Log("complete called");
        Managers.UI.CloseUI<GameBootStrapUI>();

        Managers.Scene.TransitionScene(Define.INGAME_SCENE_ID, callback: () => {
            InitGameData();
        });
    }

    private static void InitGameData() {

        // time
        Managers.Time.SetAsDefaultTime();
        Managers.Time.PauseMainTime(false);

        // stats
        var playerData = Managers.Player.PlayerData;
        playerData.MetabolicSystem.InitMetabolism(BuildMetabolism());


        // location
        Managers.Player.PlayerData.SetAsDefaultLocation();
        Managers.Location.OnLocationChanged -= playerData.SetCurrentLocation;
        Managers.Location.OnLocationChanged += playerData.SetCurrentLocation;
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
