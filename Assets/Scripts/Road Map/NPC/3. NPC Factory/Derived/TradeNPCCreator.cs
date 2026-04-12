using UnityEngine;

public class TradeNPCCreator : NPCCreatorBase<TradeNPCData, TradeNPC>
{
    public override TradeNPC CreateNPC(TradeNPCData data) {
        return new TradeNPC(data);
    }
}
