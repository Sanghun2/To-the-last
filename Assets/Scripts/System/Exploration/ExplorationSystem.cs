using System;
using UnityEngine;

public class ExplorationSystem
{
    public Location CurrentLocation
    {
        get
        {
            string currentLocationID = Managers.Player.PlayerData.CurrentLocationID;

            return string.IsNullOrEmpty(currentLocationID) ? null : GetLocation(currentLocationID);
        }
    }

    public void ExploreCurrentLocation() {
        var location = CurrentLocation;
        ExploreLocation(location);
    }
    public void ExploreLocation(Location location) {
        if (location == null) { Debug.LogError($"<color=red>location null. failed to explore.</color>"); return; }

        EncounterEvent nextEncounterData = GetNextEncouter(location);
        if (nextEncounterData != null) {
            Debug.Log("encounter 실행 구현");
            Managers.Encounter.ExecuteEncounter(nextEncounterData.EncounterSD);
        }

        // 이 부분을 처리하고 다음 단계로 넘아가는 부분을 어떻게 관리할 것인가 진행중인 encounter가 끝난처리
        // 획득한 아이템은 어떻게 처리할 것인가? 베리드 타운 way? new way?
    }

    public void QuitLocation() {
        Managers.UI.CloseUI<ExplorationUI>();
        Managers.UI.CloseUI<DialogUI>();
    }


    private Location GetLocation(string locationID) {
        if (Managers.Location.TryGetLocation(locationID, out Location location)) {
            return location;
        }

        return null;
    }
    private EncounterEvent GetNextEncouter(Location location) {
        var eventList = location.Data.LocationEventList;
        int currentProgress = location.CurrentValue;
        EncounterEvent targetEncounter = eventList[currentProgress-1];
        return targetEncounter;
    }
}
