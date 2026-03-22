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

    public override void InitUI() {
        if (IsInit) return;

        Managers.Character.OnCharacterSelected -= ShowCharacter;
        Managers.Character.OnCharacterSelected += ShowCharacter;

        var characterList = Managers.Character.GetCharacterList();
        Managers.Character.CurrentSelectedCharacterID = characterList[0].Data.CharacterID;
        InitCharacterButtons(characterList);

        _isInit = true;
    }

    public void ShowCharacter(string characterID) {
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

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{characterSD.Description}").AppendLine();

        if (features != null) {
            for (int i = 0; i < features.Length; i++) {
                var feature = features[i];
                sb.AppendLine($"● {feature}");
            }
        }

        return sb.ToString();
    }
}
