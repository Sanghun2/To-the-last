using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class DialogData
{
    public LocationSD LocationSD => locationSD;
    public Sprite CharacterImage => characterImage;
    public string CharacterName => characterName;
    public string Description => description;
    public IReadOnlyList<SelectionData> Selections => selections;

    [SerializeField] LocationSD locationSD;
    [SerializeField] Sprite characterImage;
    [SerializeField] string characterName;
    [SerializeField] string description;
    private IReadOnlyList<SelectionData> selections;

    public DialogData(LocationSD locationSD, Sprite characterImage, string characterName, string description, IReadOnlyList<SelectionData> selections) {
        this.locationSD = locationSD;
        this.characterImage = characterImage;
        this.characterName = characterName;
        this.description = description;
        this.selections = selections;
    }
}

public class DialogUI : UIBase
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image characterImage;
    [SerializeField] TextUI characterNameText;
    [SerializeField] TextUI dialogText;
    [SerializeField] SelectionButtonContainer selectionButtonContainer;

    public void ShowDialog(DialogData dialogData) {
        backgroundImage.sprite = dialogData.LocationSD.MainImage;
        characterImage.sprite = dialogData.CharacterImage;
        characterNameText.SetText(dialogData.CharacterName);
        dialogText.SetText(dialogData.Description);
        ShowSelections(dialogData.Selections);
    }


    private void Reset() {
        if (selectionButtonContainer == null) {
            selectionButtonContainer = GetComponentInChildren<SelectionButtonContainer>();
        }
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
