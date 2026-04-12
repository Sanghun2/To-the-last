using System;
using UnityEngine;

public class TradeNPC : NPCBase
{
    private TradeNPCData data;

    public TradeNPC(TradeNPCData data) : base(data.ID) {
        this.data = data;
    }


    public override void InitNPC() {

    }
    public override void ActiveNPC() {
        if (Managers.MapMarker.TryGet<TradeNPCMarkerUI, TradeNPCMarkerUIContainer>(out var container)) {
            TradeNPCMarkerUI markerUI = container.GetObj();
            markerUI.SetPosition(LocationUtility.GenerateRandomLocationCoordinate());
            markerUI.InitMarker(data.IconImage, OpenInfoPopUp);
        }
    }
    public override void ReleaseNPC() {
        throw new System.NotImplementedException();
    }

    private void OpenInfoPopUp() {
        if (Managers.Player.PlayerData.CurrentLocationID.Equals(data.ID)) {
            
        }
    }

}
