using BilliotGames;
using UnityEngine;

public class EncounterDataParserContainer : TypeRegistry<EncounterSDBase, EncounterDataParserBase>
{
    public EncounterDataParserContainer() {
        Register<LootEncounterSD>(new LootEncounterDataParser());
        Register<BattleEncounterSD>(new BattleEncounterDataParser());
        Register<DialogEncounterSD>(new DialogEncounterDataParser());
        //Register<SpecialEncounterSD>(new specialencounterdatapar());
    }
}
