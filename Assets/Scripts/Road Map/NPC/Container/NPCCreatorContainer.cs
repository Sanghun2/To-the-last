using BilliotGames;
using UnityEngine;

public sealed class NPCCreatorContainer : TypeRegistry<NPCDataBase, NPCFactoryBase>
{
    public NPCCreatorContainer() {
        Register<NPCData>(new NPCFactory());
    }
}
