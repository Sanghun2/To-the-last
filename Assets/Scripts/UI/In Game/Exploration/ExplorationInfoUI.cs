using BilliotGames;
using TMPro;
using UnityEngine;

public class ExplorationInfoUI : UIBase
{
    [SerializeField] TextMeshProUGUI locationNameText;

    public void InitInfoUI(ExplorationLocation location) {
        locationNameText.text = location.LocationName;
    }
}
