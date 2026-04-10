using BilliotGames;
using UnityEngine;

public class EncounterExecutorContainer : TypeRegistry<EncounterContextBase, EncounterExecutorBase>
{
    public EncounterExecutorContainer() {
        Register<LootEncounterContext>(new LootEncounterExecutor());
        Register<BattleEncounterContext>(new BattleEncounterExecutor());
        Register<DialogEncounterContext>(new DialogEncounterExecutor());
    }
}
