using UnityEngine;

public class LocationMarkerDataGenerator : MarkerDataGeneratorBase<ExplorationLocation, ExplorationLocationMarkerPopUpData>
{
    public override ExplorationLocationMarkerPopUpData GenerateData(ExplorationLocation location) {
        return new ExplorationLocationMarkerPopUpData(
            null
            );
    }
}
