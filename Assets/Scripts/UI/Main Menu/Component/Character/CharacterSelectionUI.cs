using System;
using System.Collections.Generic;
using System.Text;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public sealed class CharacterSelectionUI : UIBase
{
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] CharacterUIContainer characterUIContainer; 
    private readonly StringBuilder sb = new StringBuilder();

    public override void InitUI() {
        if (IsInit) return;

        _isInit = true;

        CloseUI();
    }

    public void ShowCharacter(string characterID, string prevCharacterID) {
        if (Managers.SD.TryGetSD(characterID, out CharacterSD characterSD)) {
            characterNameText.text = characterSD.DisplayText;
            characterImage.sprite = characterSD.Image;
            descriptionText.text = BuildCharacterDescription(characterSD);
        }
    }

    public void InitCharacterButtons(IReadOnlyList<Character> characterList) {
        characterUIContainer.Clear();
        for (int i = 0; i < characterList.Count; i++) {
            var character = characterList[i];
            var ui = characterUIContainer.GetOrCreateObj(i);
            ui.InitUI(character);
        }
    }

    private string BuildCharacterDescription(CharacterSD characterSD) {
        string[] features = characterSD.Features;

        sb.Clear();
        sb.AppendLine($"{characterSD.Description}").AppendLine();

        if (features != null) {
            for (int i = 0; i < features.Length; i++) {
                var feature = features[i];
                sb.AppendLine($"● {feature}");
            }
        }

        return sb.ToString();
    }

    protected override void OnOpen() {
        Managers.Character.OnCharacterSelected -= ShowCharacter;
        Managers.Character.OnCharacterSelected += ShowCharacter;

        InitCharacterButtons(Managers.Character.CharacterList);
        Managers.Character.SelectDefaultCharacter();
    }

    protected override void OnClose() {
        Managers.Character.OnCharacterSelected -= ShowCharacter;
    }
}
