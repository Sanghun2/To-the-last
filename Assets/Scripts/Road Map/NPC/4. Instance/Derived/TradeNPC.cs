using System;
using UnityEngine;

public class TradeNPC : NPCBase
{
    private float currentAffinity;
    private float maxAffinity;
    private TradeNPCData data;

    public float CurrentAffinity => currentAffinity;
    public float MaxAffinity => maxAffinity;
    public Vector2 AnchoredPosition => anchoredPosition;

    private Vector2 anchoredPosition;

    public TradeNPC(TradeNPCData data) : base(data.ID) {
        this.data = data;
        currentAffinity = 0;
        maxAffinity = data.MaxAffinity;
    }


    public override void InitNPC() {

    }
    public override void ActiveNPC() {
        if (Managers.MapMarker.TryGet<TradeNPCMarkerUI, TradeNPCMarkerUIContainer>(out var container)) {
            TradeNPCMarkerUI markerUI = container.GetObj();
            anchoredPosition = LocationUtility.GenerateRandomLocationCoordinate();
            markerUI.SetPosition(anchoredPosition);
            markerUI.InitMarker(data.IconImage, OpenInfoPopUp);
        }
    }
    public override void ReleaseNPC() {
        throw new System.NotImplementedException();
    }

    private void OpenInfoPopUp() {
        if (!Managers.Location.TryGetLocation(ID, out var location)) { return; }

        var ui = Managers.UI.GetUI<MarkerInfoPopUpUI>();
        ui.InitPopUp(Managers.Location.GenerateMarkerData(location));
        Managers.UI.OpenUI(ui);
    }
}
