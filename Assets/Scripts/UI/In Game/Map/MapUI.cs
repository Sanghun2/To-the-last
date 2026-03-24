using BilliotGames;
using UnityEngine;

public class MapUI : UIBase
{
    public LocationPointer LocationPointer
    {
        get
        {
            if (_locationPointer == null) {
                _locationPointer = FindAnyObjectByType<LocationPointer>(FindObjectsInactive.Include);  
                if (_locationPointer == null) { Debug.LogError($"<color=red>failed to find location pointer</color>"); }
            }

            return _locationPointer;
        }
    }

    private LocationPointer _locationPointer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        LocationPointer.SetPosiion(Managers.Player.PlayerData.CurrentLocationID);

        _isInit = true;
    }
}
