using System;
using UnityEngine;

public class EncounterTester : MonoBehaviour
{
    public LocationInfoSD testLocationInfo;

    public void UnlockMainLocation() {
        if (Managers.Location.TryUnlockMainLocation(testLocationInfo.ID)) {
            Managers.Toast.ShowToast($"새로운 지역 발견 [{testLocationInfo.DisplayText}]", Toast.Type.Confirm);
        }
    }

    public void UnlockSubLocation() {
        var coordinate = Managers.Location.CreateNewLocationCoordinate(testLocationInfo);
        if (Managers.Location.TryUnlockSubLocation(coordinate)) {
            Managers.Toast.ShowToast($"새로운 지역 발견 [{testLocationInfo.DisplayText}]", Toast.Type.Confirm);
        }
    }
}
