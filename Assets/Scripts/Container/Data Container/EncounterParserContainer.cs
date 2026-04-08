using BilliotGames;
using UnityEngine;

public class EncounterParserContainer : TypeRegistry<EncounterSDBase, EncounterParserBase>
{
    public EncounterParserContainer() {
        Register<LootEncounterSD>(new LootEncounterParser());
    }
}
