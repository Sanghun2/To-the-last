using UnityEngine;

public class NPCDataParser : NPCDataParserBase<NPCSD, NPCData>
{
    public override NPCData ParseData(NPCSD npcSD) {
        return new NPCData(npcSD.ID);
    }
}
