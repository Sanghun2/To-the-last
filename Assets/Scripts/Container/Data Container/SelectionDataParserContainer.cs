using BilliotGames;
using UnityEngine;

public class SelectionDataParserContainer : TypeRegistry<SelectionRunnerSDBase, SelectionRunnerDataParserBase>
{
    public SelectionDataParserContainer() {
        Register<LootSelectionRunnerSD>(new LootSelectionRunnerDataParser());
    }
}
