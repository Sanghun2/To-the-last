using BilliotGames;
using UnityEngine;

public class EncounterParserContainer : TypeRegistry<EncounterSD, EncounterParserBase>
{
    public EncounterParserContainer() {
        Register<LootEncounterSD>(new LootEncounterParser());
    }
}
