using UnityEngine;

public class NPCDataParser : NPCDataParserBase<NPCSD, TradeNPCData>
{
    public override TradeNPCData ParseData(NPCSD npcSD) {
        return new TradeNPCData(npcSD.ID);
    }
}
