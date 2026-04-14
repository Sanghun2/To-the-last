using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationData
{
    //public ExplorationLocation ExplorationLocation => location;
    //public Location Location => location == null ? null : location.Location;
    public EncounterDataBase CurrentEvent => location.LocationEventList[location.CurrentValue];

    [SerializeField] ExplorationLocation location;

    public ExplorationData(ExplorationLocation location) {
        this.location = location;
    }
}

public class ExplorationUI : LocationUIBase<ExplorationLocation>
{

    [SerializeField] Image mainBackgroundImage;
    [SerializeField] GameObject siuationObj;
    [SerializeField] Image eventImage;
    [SerializeField] TextUI descriptionText;
    [SerializeField] SelectionButtonContainer selectionButtonContainer;
    [SerializeField] ExplorationInfoUI explorationInfoUI;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public override void InitLocationUI(ExplorationLocation location) {
        InitUI();
        EnteranceUI.InitEnteracne(location, new ActionData("탐색한다", () => Managers.Exploration.ContinueToExploreCurrentLocation()));

        mainBackgroundImage.sprite = location.Data.MainImage;
        HideSituation();
        explorationInfoUI.InitInfoUI(location);
    }
    public override void ShowEnterance() {
        base.ShowEnterance();
        siuationObj.SetActive(false);
    }
    public void ShowSituation(EncounterDataBase encounterData) {
        InitUI();
        EnteranceUI.CloseUI();

        ShowSituationImage(encounterData.EventImage);
        descriptionText.SetText(encounterData.Description);
        ShowSelections(encounterData.SelectionList.Select(selectionSDContext => {
            if (Managers.Select.TryBuildSelectionContext(selectionSDContext, out SelectionContext selectionContext)) {
                return selectionContext;
            }
            else {
                Debug.LogError($"failed to build select action");
                return null;
            }
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
    private void ShowSelections(IReadOnlyList<SelectionContext> selections) {
        selectionButtonContainer.Clear();
        var container = selectionButtonContainer;
        for (int i = 0; i < selections.Count; i++) {
            SelectionContext selection = selections[i];
            var button = container.GetOrCreateObjOf(i);
            button.InitButton(selection);
        }
    }

    private void HideSituation() {
        siuationObj.SetActive(false);
    }
}
