using System;
using UnityEngine;

public class EncounterTester : MonoBehaviour
{
    public LocationInfoSD testLocationInfo;

    public void UnlockMainLocation() {
        if (Managers.Location.TryUnlockMainLocation(testLocationInfo.ID, 1, out var location)) {
            Managers.Toast.ShowToast($"새로운 지역 발견 [{location.LocationName}]", Toast.Type.Confirm);
        }
    }

    public void UnlockSubLocation() {
        var coordinate = Managers.Location.CreateNewLocationCoordinate(testLocationInfo);
        if (Managers.Location.TryUnlockSubLocation(coordinate, out var location)) {
            Managers.Toast.ShowToast($"새로운 지역 발견 [{location.LocationName}]", Toast.Type.Confirm);
        }
    }
}
