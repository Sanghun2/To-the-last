using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class LocationInventoryUI : UIBase
{
    [SerializeField] ItemStorageInventoryUI topInventoryUI;
    [SerializeField] ItemStorageInventoryUI bottomInventoryUI;
    [SerializeField] CustomButtonContainer customButtonContainer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public void ShowInventory(string locationID, Exploration.State state) {
        if (!Managers.Inventory.TryGetInventoryByID(locationID, out InventoryBase locationInventory)) {
            locationInventory = Managers.Inventory.AddInventory(new SimpleInventory(locationID, 50));
        }

        var playerInven = Managers.Player.PlayerData.Inventory;

        ShowInventory(locationInventory, playerInven, CreateButtonActions(state));
    }

    public void ShowInventory(InventoryBase top, InventoryBase bottom, IReadOnlyList<ActionData> buttons) {
        InitUI();

        topInventoryUI.ShowInventory(top);
        bottomInventoryUI.ShowInventory(bottom);

        customButtonContainer.InitButtons(buttons);

        OpenUI();
    }

    private ActionData[] CreateButtonActions(Exploration.State state) {
        switch (state) {
            case Exploration.State.Enterance:
                return new ActionData[] {
                    new ActionData(
                        "확인",
                        () => {
                            Managers.UI.CloseUI<LocationInventoryUI>();
                        })
                };
            case Exploration.State.Exploring:
                return new ActionData[] {
                    new ActionData(
                        "나간다",
                        () => {
                            Managers.UI.CloseUI<LocationInventoryUI>();
                            Managers.Exploration.GoToEnterance();
                        }
                        ),
                    new ActionData(
                        "탐색한다",
                        () => {
                            Managers.UI.CloseUI<LocationInventoryUI>();
                            Managers.Exploration.ContinueToExploreCurrentLocation();
                        })
                };
            default:
                return null;
        }
    }
}
