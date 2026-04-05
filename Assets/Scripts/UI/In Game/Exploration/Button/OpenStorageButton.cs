using BilliotGames;
using UnityEngine;

public class OpenStorageButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.Exploration.OpenCurrentStorage();
    }
}
