using System;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateData
{
    public string LocationID => locationID; 
    public string LocationName => locationName;
    public Vector2 LocationCoordinate => locationCoordinate;
    public float TargetHz => radioHz;

    public string DisplayText => displayText;
    public string StoryDescription => storyDescription;
    public Sprite MainImage => mainImage;
    public Sprite IconImage => iconImage;
    public IReadOnlyList<EncounterEvent> LocationEventList { get; internal set; }

    private float radioHz;

    private string locationID;
    private string locationName;
    private Vector2 locationCoordinate;
    private string displayText;
    private string storyDescription;
    private Sprite iconImage;
    private Sprite mainImage;

    public CoordinateData(
        string locationID, 
        string locationName,
        Vector2 locationCoordinate, 
        float radioHz,
        string displayText,
        string storyDescription,
        Sprite iconImage,
        Sprite mainImage) {

        this.locationID = locationID;
        this.locationName = locationName;
        this.locationCoordinate = locationCoordinate;
        this.radioHz = radioHz;
        this.iconImage = iconImage;
        this.mainImage = mainImage;
        this.storyDescription = storyDescription;
        this.displayText = displayText;
    }

    public bool IsHzMatched(float currentHz) {
        return Mathf.Approximately(currentHz, radioHz);
    }
}
