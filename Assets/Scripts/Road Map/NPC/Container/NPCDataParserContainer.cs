using BilliotGames;
using UnityEngine;

public class NPCDataParserContainer : TypeRegistry<NPCSDBase, NPCDataParserBase>
{
    public NPCDataParserContainer() {
        Register<TradeNPCSD>(new NPCDataParser());
    }
}
