using BillotGames;
using UnityEngine;

public class SelectActionConverterContainer : TypeRegistry<ActionContextBase,  SelectActionConverterBase>
{
    public SelectActionConverterContainer() {
        Register<LootSelectActionContext>(new LootSelectActionConverter());
    }
}
