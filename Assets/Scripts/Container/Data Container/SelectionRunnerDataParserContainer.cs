using BilliotGames;
using UnityEngine;

public class SelectionRunnerDataParserContainer : TypeRegistry<SelectionRunnerSDBase, SelectionRunnerDataParserBase>
{
    public SelectionRunnerDataParserContainer() {
        Register<LootSelectionRunnerSD>(new LootSelectionRunnerDataParser());
    }
}
