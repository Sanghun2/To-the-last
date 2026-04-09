using BilliotGames;
using UnityEngine;

public class EncounterContextBuilderContainer : TypeRegistry<EncounterDataBase, IEncounterContextBuilder>
{
    public EncounterContextBuilderContainer() {
        Register<LootEncounterData>(new LootEncounterContextBuilder());
        Register<BattleEncounterData>(new BattleEncounterContextBuilder());
    }
}
