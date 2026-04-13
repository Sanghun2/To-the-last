using BilliotGames;
using UnityEngine;

public class MarkerDataGeneratorContainer : TypeRegistry<LocationBase, MarkerDataGeneratorBase>
{
    public MarkerDataGeneratorContainer() {
        Register<ExplorationLocation>(new LocationMarkerDataGenerator());
        Register<TradeNPCLocation>(new TradeNPCMarkerDataGenerator());
    }
}
