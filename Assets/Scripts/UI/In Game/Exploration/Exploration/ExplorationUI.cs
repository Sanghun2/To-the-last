using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationData
{
    //public Location Location => location;
    //public Data Data => location == null ? null : location.Data;
    public EncounterDataBase CurrentEvent => location.Data.LocationEventList[location.CurrentValue];

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
    [SerializeField] ExplorationInfoUI explorationInfoUI;
    private EnteranceUI _enteranceUI;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public void InitLocationUI(Location location) {
        InitUI();
        mainBackgroundImage.sprite = location.Data.MainImage;
        HideSituation();
        explorationInfoUI.InitInfoUI(location);
    }
    public void ShowEnterance() {
        InitUI();
        EnteranceUI.InitButtons();
        EnteranceUI.OpenUI();
        siuationObj.SetActive(false);
    }
    public void ShowSituation(EncounterDataBase encounterData) {
        InitUI();
        EnteranceUI.CloseUI();

        ShowSituationImage(encounterData.EventImage);
        descriptionText.SetText(encounterData.Description);
        ShowSelections(encounterData.SelectionList.Select(selectionPair => {
            if (Managers.Select.TryBuildSelectionContext(selectionPair, out var selectionContext)) {
                return new SelectActionData(selectionContext);
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
    private void ShowSelections(IReadOnlyList<SelectActionData> selections) {
        selectionButtonContainer.Clear();
        var container = selectionButtonContainer;
        for (int i = 0; i < selections.Count; i++) {
            SelectActionData selectionData = selections[i];
            var button = container.GetOrCreateObjOf(i);
            button.InitButton(selectionData);
        }
    }

    private void HideSituation() {
        siuationObj.SetActive(false);
    }
}
