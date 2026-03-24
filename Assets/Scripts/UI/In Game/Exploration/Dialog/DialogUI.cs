using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class DialogData
{
    public Sprite CharacterImage => characterImage;
    public string CharacterName => characterName;
    public string Description => description;
    public IReadOnlyList<SelectionData> Selections => selections;

    [SerializeField] Sprite characterImage;
    [SerializeField] string characterName;
    [SerializeField] string description;
    private IReadOnlyList<SelectionData> selections;

    public DialogData( Sprite characterImage, string characterName, string description, IReadOnlyList<SelectionData> selections) {
        this.characterImage = characterImage;
        this.characterName = characterName;
        this.description = description;
        this.selections = selections;
    }
}

public class DialogUI : UIBase
{
    [SerializeField] Image characterImage;
    [SerializeField] TextUI characterNameText;
    [SerializeField] TextUI dialogText;
    [SerializeField] SelectionButtonContainer selectionButtonContainer;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public void ShowDialog(DialogData dialogData) {
        CloseUI();
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
        selectionButtonContainer.Clear();
        var container = selectionButtonContainer;
        for (int i = 0; i < selections.Count; i++) {
            var selectionData = selections[i];
            var button = container.GetOrCreateObj(i);
            button.InitButton(selectionData.Text, selectionData.Action, new SelectionButtonContext()
                .SetLock(false)
                .SetRequirement(selectionData.Requirement));
        }
    }
}
