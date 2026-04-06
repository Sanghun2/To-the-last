using BilliotGames;
using UnityEngine;

public sealed class NPCFactoryContainer : TypeRegistry<NPCDataBase, NPCFactoryBase>
{
    public NPCFactoryContainer() {
        Register<NPCData>(new NPCFactory());
    }
}
