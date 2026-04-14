using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnteranceUI : UIBase
{
    [SerializeField] BackButton backButton;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image mainImage;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] CustomButtonContainer buttonContainer;

    public override void InitUI() {
        if (IsInit) return;

        backButton.InitUI();
        buttonContainer.InitUI();
        backButton.SetButtonAction(QuitLocation);

        _isInit = true;
    }

    public void InitEnteracne(LocationBase location,params ActionData[] actions) {
        nameText.text = location.DisplayName;
        mainImage.sprite = location.MainImage;
        description.text = location.StoryDescription;
        InitButtons(actions);
    }

    private void InitButtons(ActionData[] additionalActions) {
        InitUI();
        var actions = new List<ActionData> { new ActionData("나간다", QuitLocation) };
        actions.AddRange(additionalActions);
        buttonContainer.InitButtons(actions);
    }

    private void QuitLocation() {
        Managers.Exploration.ExitLocation();
    }

    private void Reset() {
        if (buttonContainer == null) {
            buttonContainer = GetComponentInChildren<CustomButtonContainer>();
        }
    }
}
