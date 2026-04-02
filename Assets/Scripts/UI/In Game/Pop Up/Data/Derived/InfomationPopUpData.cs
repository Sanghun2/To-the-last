using UnityEngine;

public class InfomationPopUpData : PopUpDataBase, 
    ITitleContent, 
    IDescriptionContent, 
    ISubTextContent, 
    IImageContent
{
    public string Title { get; }
    public string SubText { get; }
    public Sprite Image { get; }
    public string Description { get; }

    public InfomationPopUpData(
        string mainText, 
        string description, 
        ActionData[] buttonActions, 
        string subText = null, 
        Sprite image = null) 
        : base(buttonActions) {

        Title = mainText;
        Image = image;
        SubText = subText;
        Description = description;
    }
}
