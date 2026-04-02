using UnityEngine;

public class PopUpData : PopUpDataBase, ITitleContent, IDescriptionContent
{
    public string Title { get; }
    public string Description { get; }

    public PopUpData(string title, string description, ActionData[] buttonActions)
        : base(buttonActions) {

        Title = title;
        Description = description;
    }

    public void SetButtonActions(ActionData[] buttonActions) {
        this.buttonActions = buttonActions;
    }
}
