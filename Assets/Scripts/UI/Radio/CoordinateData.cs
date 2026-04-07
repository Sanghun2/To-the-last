using System;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateData
{
    public string LocationUID => locationUID;
    public string LocationCategoryID => locationCategoryID;
    public string LocationName => locationName;
    public Vector2 LocationCoordinate => locationCoordinate;
    public float TargetHz => radioHz;

    private float radioHz;

    private string locationUID;
    private string locationCategoryID;
    private string locationName;
    private Vector2 locationCoordinate;

    public CoordinateData(
        string locationID, 
        string locationCategoryID,
        string locationName,
        Vector2 locationCoordinate, 
        float radioHz) {

        this.locationUID = locationID;
        this.locationCategoryID = locationCategoryID;
        this.locationName = locationName;
        this.locationCoordinate = locationCoordinate;
        this.radioHz = radioHz;
    }

    public bool IsHzMatched(float currentHz) {
        return Mathf.Approximately(currentHz, radioHz);
    }
}
