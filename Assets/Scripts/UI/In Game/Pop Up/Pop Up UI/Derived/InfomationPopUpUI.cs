using UnityEngine;

public class InfomationPopUpUI : PopUpUIBase
{
    [SerializeField] TouchClosePanel closePanel;
    [SerializeField] RectRebuilder rectRebuilder;

    public override void InitPopUp(PopUpDataBase popUpData) {
        base.InitPopUp(popUpData);

        closePanel.SetCloseAction(popUpData.OnCloseByPanel);

        rectRebuilder.Rebuild();
    }

    public void SetSubText(string text) {
        var obj = subText.gameObject;
        if (!obj.activeSelf) obj.SetActive(true);
        subText.text = text;
    }
}
