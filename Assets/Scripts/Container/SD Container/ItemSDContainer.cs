using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class ItemSDContainer : SDContainerBase<ItemSD>
{
    public ItemSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class ProductionSDContainer : SDContainerBase<ProductionContentSD>
{
    public ProductionSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class StructureSDContainer : SDContainerBase<StructureSDBase>
{
    public StructureSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class LocationSDContainer : SDContainerBase<LocationSD>
{
    public LocationSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class CharacterSDContiner : SDContainerBase<CharacterSD>
{
    public CharacterSDContiner(string sdResourcePath) : base(sdResourcePath) {
    }
}

public class EncounterSDContainer : SDContainerBase<EncounterSD>
{
    public EncounterSDContainer(string sdResourcePath) : base(sdResourcePath) {
    }
}