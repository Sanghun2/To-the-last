using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationData
{
    //public Location Location => location;
    //public LocationSD LocationSD => location == null ? null : location.LocationSD;
    public EncounterEvent CurrentEvent => location.LocationSD.LocationEventList[location.CurrentValue];

    [SerializeField] Location location;

    public ExplorationData(Location location) {
        this.location = location;
    }
}

public class ExplorationUI : UIBase
{
    public EnteranceUI EnteranceUI
    {
        get
        {
            if (_enteranceUI == null) {
                _enteranceUI = Managers.UI.GetUI<EnteranceUI>();
            }

            return _enteranceUI;
        }
    }

    [SerializeField] Image mainBackgroundImage;
    [SerializeField] GameObject siuationObj;
    [SerializeField] Image eventImage;
    [SerializeField] TextUI descriptionText;
    [SerializeField] SelectionButtonContainer selectionButtonContainer;
    private EnteranceUI _enteranceUI;

    public void InitLocationUI(Location location) {
        mainBackgroundImage.sprite = location.LocationSD.MainImage;
        HideSituation();
    }
    public void ShowEnterance() {
        EnteranceUI.InitButtons();
        EnteranceUI.OpenUI();
        siuationObj.SetActive(false);
    }
    public void ShowSituation(EncounterSD encounterSD) {
        EnteranceUI.CloseUI();
        ShowSituationImage(encounterSD.EventImage);
        descriptionText.SetText(encounterSD.Description);
        ShowSelections(encounterSD.SelectionList.Select(s => {
            Action action = Managers.ActionCreator.CreateActionData(new SelectionActionContext(s, s.RequireMinutes)).Action;
            return new SelectionData(s, action);
        }).ToList());
        siuationObj.SetActive(true);
    }

    private void Reset() {
        if (selectionButtonContainer == null) {
            selectionButtonContainer = GetComponentInChildren<SelectionButtonContainer>();
        }
    }

    private void ShowSituationImage(Sprite image) {
        eventImage.sprite = image;
        eventImage.gameObject.SetActive(image != null);
    }
    private void ShowSelections(IReadOnlyList<SelectionData> selections) {
        selectionButtonContainer.ReleaseContainer();
        var container = selectionButtonContainer;
        for (int i = 0; i < selections.Count; i++) {
            var selectionData = selections[i];
            var button = container.GetObj(i);
            button.InitButton(selectionData.Text, selectionData.Action, new SelectionButtonContext()
                .SetLock(false)
                .SetRequirement(selectionData.Requirement));
        }
    }

    private void HideSituation() {
        siuationObj.SetActive(false);
    }
}
