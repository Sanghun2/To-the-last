using BilliotGames;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;


public class LocationInfoPopUpData : PopUpData
{
    public Location Location => location;

    [SerializeField] Location location;

    public LocationInfoPopUpData(string title, string description, ActionData[] buttonActions) : base(title, description, buttonActions) {
    }


    public LocationInfoPopUpData(Location location, ActionData[] buttonActions) : base (location.LocationSD.DisplayName, location.LocationSD.StoryDescription, buttonActions){
        this.location = location;
    }
}

public class LocationInfoPopUpUI : PopUpUIBase<LocationInfoPopUpData>
{
    [SerializeField] protected Image locationImage;
    [SerializeField] protected TextUI progressText;

    public override void InitPopUp(LocationInfoPopUpData popUpData) {
        base.InitPopUp(popUpData);
        var sd = popUpData.Location.LocationSD;
        locationImage.sprite = sd.MainImage;

        int currentProgress = popUpData.Location.CurrentValue;
        bool progressShow = currentProgress > 0;
        if (progressShow) {
            progressText.SetText($"진행도 {popUpData.Location.CurrentValue}/{sd.LocationEventList.Count}");            
        }
        progressText.gameObject.SetActive(progressShow);
    }
}
