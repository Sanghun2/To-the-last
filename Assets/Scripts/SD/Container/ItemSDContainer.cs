using BilliotGames;
using UnityEngine;

public class ItemSDContainer : SDContainerBase<ItemSD>
{
    public ItemSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class RecipeSDContainer : SDContainerBase<RecipeSD>
{
    public RecipeSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class StructureSDContainer : SDContainerBase<StructureSD>
{
    public StructureSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}
