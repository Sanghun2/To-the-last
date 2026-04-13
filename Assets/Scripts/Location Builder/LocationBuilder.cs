using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuilder
{
    private EncounterMapBuilder encounterContentBuilder = new EncounterMapBuilder();

    public bool TryBuildxplorationLocation(LocationBuildContext locationContext, out ExplorationLocation newLocation) {
        newLocation = null;
        if (!Managers.SD.TryGetSD(locationContext.LocationCategoryID, out LocationInfoSD infoSD)) { Debug.LogError($"<color=red>infoSD of ({locationContext.LocationCategoryID}) is not exist</color>"); return false; }

        var newLocationData = new LocationData(
            locationContext.LocationUID,
            locationContext.LocationCategoryID,
            locationContext.DisplayName,
            infoSD.Description,
            locationContext.AnchoredPosition,
            infoSD.Image,
            infoSD.IconImage);

        newLocation = new ExplorationLocation(newLocationData, BuildLocationEvents(locationContext));

        int currentProgress = locationContext.CurrentProgress;
        int maxProgress = locationContext.EncounterDataList.Count;
        newLocation.InitProgress(currentProgress, maxProgress);
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
