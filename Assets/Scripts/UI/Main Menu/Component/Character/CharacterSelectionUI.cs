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

    public void ShowCharacter(string characterID) {
        if (Managers.SD.TryGetSD(characterID, out CharacterSD characterSD)) {
            characterNameText.text = characterSD.DisplayText;
            characterImage.sprite = characterSD.Image;
            descriptionText.text = BuildCharacterDescription(characterSD);
        }
    }

    public void InitCharacterList(IReadOnlyList<CharacterData> characterDataList) {
        characterUIContainer.Clear();
        for (int i = 0; i < characterDataList.Count; i++) {
            var data = characterDataList[i];
            var ui = characterUIContainer.GetOrCreateObj(i);
            ui.InitUI(data);
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
