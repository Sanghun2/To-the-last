using BilliotGames;
using UnityEngine;

public class MapUI : UIBase
{
    public LocationPointer LocationPointer
    {
        get
        {
            if (_locationPointer == null) {
                _locationPointer = GetComponentInChildren<LocationPointer>(true);  
                if (_locationPointer == null) { Debug.LogError($"<color=red>failed to find location pointer</color>"); }
            }

            return _locationPointer;
        }
    }

    private LocationPointer _locationPointer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    private void OnEnable() {
        LocationPointer.SetPosiion(Managers.Player.PlayerData.CurrentLocationID);
    }
}
