using BilliotGames;
using UnityEngine;

public class SelectionContextBuilderContainer : TypeRegistry<SelectionRunnerDataBase, SelectionContextBuilderBase>
{
    public SelectionContextBuilderContainer() {
        Register<LootSelectionRunnerData>(new LootSelectionContextBuilder());
    }
}
