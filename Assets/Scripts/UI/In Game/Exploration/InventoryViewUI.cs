using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class InventoryViewUI : UIBase
{
    [SerializeField] ItemStorageInventoryUI topInventoryUI;
    [SerializeField] ItemStorageInventoryUI bottomInventoryUI;
    [SerializeField] BackButton backButton;
    [SerializeField] CustomButtonContainer customButtonContainer;

    public event Action OnClosed;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        backButton.InitUI();
        backButton.SetButtonAction(() => {
            Managers.UI.CloseUI(this);
            OnClosed?.Invoke();
        });

        _isInit = true;
    }

    public void ShowInventory(string locationID, Exploration.State state) {
        InitUI();
        if (!Managers.Inventory.TryGetInventoryByID(locationID, out InventoryBase locationInventory)) {
            locationInventory = Managers.Inventory.AddInventory(new SimpleInventory(locationID, 50));
        }

        var playerInven = Managers.Player.PlayerData.Inventory;

        ShowInventory(locationInventory, playerInven, CreateButtonActions(state));
    }

    public void ShowInventory(InventoryBase top, InventoryBase bottom, IReadOnlyList<ActionData> buttons) {
        InitUI();
        topInventoryUI.InitInventory(top);
        bottomInventoryUI.InitInventory(bottom);

        topInventoryUI.ShowInventory(top);
        bottomInventoryUI.ShowInventory(bottom);

        if (customButtonContainer != null) {
            customButtonContainer.InitButtons(buttons);
        }

        OpenUI();
    }

    private ActionData[] CreateButtonActions(Exploration.State state) {
        switch (state) {
            case Exploration.State.Enterance:
                return new ActionData[] {
                    new ActionData(
                        "확인",
                        () => {
                            Managers.UI.CloseUI<InventoryViewUI>();
                        })
                };
            case Exploration.State.Exploring:
                return new ActionData[] {
                    new ActionData(
                        "나간다",
                        () => {
                            Managers.UI.CloseUI<InventoryViewUI>();
                            Managers.Exploration.GoToEnterance();
                        }
                        ),
                    new ActionData(
                        "탐색한다",
                        () => {
                            Managers.UI.CloseUI<InventoryViewUI>();
                            Managers.Exploration.ContinueToExploreCurrentLocation();
                        })
                };
            default:
                return null;
        }
    }
}
