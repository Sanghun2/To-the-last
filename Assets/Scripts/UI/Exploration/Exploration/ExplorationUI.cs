using System.Collections.Generic;
using BilliotGames;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationData
{
    public Location Location => location;
    //public LocationSD LocationSD => location == null ? null : location.LocationSD;
    public EcounterEvent CurrentEvent => location.LocationSD.LocationEventList[location.CurrentValue];

    [SerializeField] Location location;

    public ExplorationData(Location location) {
        this.location = location;
    }
}

public class ExplorationUI : UIBase
{
    [SerializeField] Image mainBackgroundImage;
    [SerializeField] Image eventBackgroundImage;
    [SerializeField] TextUI descriptionText;
    [SerializeField] SelectionButtonContainer selectionButtonContainer;

    public void ShowUI(ExplorationData explorationData) {
        var currentEvent = explorationData.CurrentEvent;
        var eventImage = currentEvent.EncounterSD.EventImage;
        mainBackgroundImage.sprite = explorationData.Location.LocationSD.MainImage;
        InitEventBackground(eventImage);
        descriptionText.SetText(currentEvent.EncounterSD.Description);

        List<SelectionData> selectionDataList = new List<SelectionData>(5);
        var selectionList = currentEvent.EncounterSD.SelectionList;
        for (int i = 0; i < selectionList.Count; i++) {
            selectionDataList.Add(new SelectionData(selectionList[i]));
        }

        ShowSelections(selectionDataList);
    }

    private void Reset() {
        if (selectionButtonContainer == null) {
            selectionButtonContainer = GetComponentInChildren<SelectionButtonContainer>();
        }
    }

    private void InitEventBackground(Sprite image) {
        eventBackgroundImage.sprite = image;
        eventBackgroundImage.gameObject.SetActive(image != null);
    }
    private void ShowSelections(IReadOnlyList<SelectionData> selections) {
        selectionButtonContainer.ReleaseContainer();
        var container = selectionButtonContainer;
        for (int i = 0; i < selections.Count; i++) {
            var selection = selections[i];
            var button = container.GetObj(i);
            button.InitButton(selection.Text, selection.Action);
        }
    }
}
