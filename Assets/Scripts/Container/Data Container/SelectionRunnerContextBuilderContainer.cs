using BilliotGames;
using UnityEngine;

public class SelectionRunnerContextBuilderContainer : TypeRegistry<SelectionRunnerDataBase, SelectionRunnerContextBuilderBase>
{
    public SelectionRunnerContextBuilderContainer() {
        Register<LootSelectionRunnerData>(new LootSelectionRunnerContextBuilder());
    }
}
