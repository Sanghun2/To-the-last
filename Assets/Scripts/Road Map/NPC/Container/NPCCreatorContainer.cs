using BilliotGames;
using UnityEngine;

public sealed class NPCCreatorContainer : TypeRegistry<NPCDataBase, NPCCreatorBase>
{
    public NPCCreatorContainer() {
        Register<QuestNPCData>(new QuestNPCCreator());
        Register<TradeNPCData>(new TradeNPCCreator());
    }
}
