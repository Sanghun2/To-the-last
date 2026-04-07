using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuilder
{
    public bool TryBuildLocation(LocationBuildContext context, out Location newLocation) {
        newLocation = null;
        if (Managers.SD.TryGetSD(context.LocationID, out LocationInfoSD infoSD)) { return false; }

        var builtLocationData = new LocationData(
            context.LocationID,
            BuildLocationEvents(),
            infoSD.DisplayText,
            infoSD.Description,
            context.AnchoredPosition,
            infoSD.Image,
            infoSD.IconImage,
            null
            );

        newLocation = new Location(builtLocationData);
        return true;
    }

    private IReadOnlyList<EncounterEvent> BuildLocationEvents() {
        throw new NotImplementedException();
    }
}
