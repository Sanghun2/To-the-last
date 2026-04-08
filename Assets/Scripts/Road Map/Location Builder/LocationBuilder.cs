using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuilder
{
    private EncounterMapBuilder encounterContentBuilder = new EncounterMapBuilder();

    public bool TryBuildLocation(LocationBuildContext locationContext, out Location newLocation) {
        newLocation = null;
        if (!Managers.SD.TryGetSD(locationContext.LocationCategoryID, out LocationInfoSD infoSD)) { Debug.LogError($"<color=red>infoSD of ({locationContext.LocationCategoryID}) is not exist</color>"); return false; }

        var builtLocationData = new LocationData(
            locationContext.LocationUID,
            locationContext.LocationCategoryID,
            BuildLocationEvents(locationContext),
            locationContext.DisplayName,
            infoSD.Description,
            locationContext.AnchoredPosition,
            infoSD.Image,
            infoSD.IconImage,
            null
            );

        newLocation = new Location(builtLocationData);
        return true;
    }

    private IReadOnlyList<EncounterDataBase> BuildLocationEvents(LocationBuildContext context) {
        return encounterContentBuilder.BuildMap(new EncounterMapContext(
            context.LocationCategoryID, 
            7,
            15,
            context.EncounterDataList));
    }
}
