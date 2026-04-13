using UnityEngine;

public class TradeNPCLocation : LocationBase
{
    public TradeNPC TradeNPC => tradeNPC;   

    private TradeNPC tradeNPC;

    public TradeNPCLocation(TradeNPC tradeNPC, LocationData data) : base(data) {
        this.tradeNPC = tradeNPC;
    }
}
