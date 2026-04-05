using System;
using BilliotGames;
using UnityEngine;

public class EnteranceUI : UIBase
{
    [SerializeField] CustomButtonContainer buttonContainer;

    public override void InitUI() {
        if (IsInit) return;

        buttonContainer.InitUI();

        _isInit = true;
    }

    public void InitButtons() {
        InitUI();
        buttonContainer.InitButtons(new ActionData[] {
            new ActionData("나간다", QuitLocation),
            new ActionData("탐색한다", ExploreLocation)
        });
    }

    private void ExploreLocation() {
        Managers.Exploration.ContinueToExploreCurrentLocation();
    }

    private void QuitLocation() {
        Managers.Exploration.ExitLocation();
    }

    private void Reset() {
        if (buttonContainer == null) {
            buttonContainer = GetComponentInChildren<CustomButtonContainer>();
        }
    }
}
