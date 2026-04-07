using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuilder
{
    public bool TryBuildLocation(LocationBuildContext context, out Location newLocation) {
        newLocation = null;
        if (!Managers.SD.TryGetSD(context.LocationCategoryID, out LocationInfoSD infoSD)) { Debug.LogError($"<color=red>infoSD of ({context.LocationCategoryID}) is not exist</color>"); return false; }

        var builtLocationData = new LocationData(
            context.LocationUID,
            context.LocationCategoryID,
            BuildLocationEvents(),
            context.DisplayName,
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
        return null;
    }
}
