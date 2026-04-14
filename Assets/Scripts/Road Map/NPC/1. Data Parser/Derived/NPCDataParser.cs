using UnityEngine;

public class NPCDataParser : NPCDataParserBase<TradeNPCSD, TradeNPCData>
{
    public override TradeNPCData ParseData(TradeNPCSD npcSD) {
        return new TradeNPCData(
            npcSD.ID, 
            npcSD.IconImage, 
            npcSD.MaxAffinity,
            npcSD.TradeItemList
            );
    }
}
