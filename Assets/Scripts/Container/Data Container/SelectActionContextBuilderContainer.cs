using BilliotGames;
using UnityEngine;

public class SelectActionContextBuilderContainer : TypeRegistry<SelectionRunnerDataBase, SelectActionContextBuilderBase>
{
    public SelectActionContextBuilderContainer() {
        Register<LootSelectionRunnerData>(new LootSelectActionContextBuilder());
    }
}
