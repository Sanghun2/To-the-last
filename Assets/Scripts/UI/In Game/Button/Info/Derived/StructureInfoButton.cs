using UnityEngine;

public class StructureInfoButton : InfoButtonBase
{
    protected override void ButtonAction() {
        ShowInfomation();
    }

    protected override void ShowInfomation() {
        if (string.IsNullOrEmpty(dataID)) { Debug.Log("타겟 아이템 없음"); return; }
        if (!Managers.SD.TryGetSD(dataID, out StructureSDBase structureSD)) { return; }

        var infoUI = Managers.UI.GetUI<InfomationPopUpUI>();
        if (infoUI.IsOpened) return;

        InfomationPopUpData infoData = new InfomationPopUpData(
            structureSD.DisplayText,
            structureSD.Description,
            new ActionData[] { new ActionData("확인", () => Managers.UI.CloseUI(infoUI)) },
            image: structureSD.Image);
        Managers.UI.OpenUI(infoUI).InitPopUp(infoData);
    }
}
