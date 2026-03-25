using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : UIBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] Image characterImage;
    [SerializeField] GameObject selectedUIObj;
    [SerializeField] CharacterSelectionButton selectionButton;
    private string characterID;


    public void InitUI(Character character) {
        var data = character.Data;
        characterID = data.CharacterID;
        characterImage.sprite = data.CharacterImage;
        selectionButton.InitCharacter(data.CharacterID, character.Locked);

        UpdateSelectUI(Managers.Character.CurrentSelectedCharacterID, null);
        characterImage.color = character.Locked ? Color.black : Color.white;
    }

    protected override void OnOpen() {
        Managers.Character.OnCharacterSelected -= UpdateSelectUI;
        Managers.Character.OnCharacterSelected += UpdateSelectUI;
    }
    protected override void OnClose() {
        Managers.Character.OnCharacterSelected -= UpdateSelectUI;
    }

    private void UpdateSelectUI(string currentCharacterID, string prevCharacterID) {
        if (characterID.Equals(currentCharacterID) || characterID.Equals(prevCharacterID))

        selectedUIObj.SetActive(characterID.Equals(currentCharacterID));
    }

    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        if (IsInit) return;

        InitUI();
        selectedUIObj.SetActive(false);

        _isInit = true;
    }
    public void Return() {
        CloseUI();
        characterID = null;
        selectedUIObj.SetActive(false);
    }

    #endregion
}
