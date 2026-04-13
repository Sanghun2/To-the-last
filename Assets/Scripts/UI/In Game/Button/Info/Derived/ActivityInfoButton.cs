using UnityEngine;

public class ActivityInfoButton : InfoButtonBase
{
    protected override void ShowInfomation() {
        if (string.IsNullOrEmpty(dataID)) { Debug.Log("타겟 아이템 없음"); return; }
        if (!Managers.SD.TryGetSD(dataID, out ActivityContentSD contentSD)) { return; }

        var infoUI = Managers.UI.GetUI<InfomationPopUpUI>();
        if (infoUI.IsOpened) return;

        InfomationPopUpData infoData = new InfomationPopUpData(
            contentSD.DisplayName,
            contentSD.Description,
            new ActionData[] { new ActionData("확인", () => Managers.UI.CloseUI(infoUI)) },
            image: contentSD.Image);
        Managers.UI.OpenUI(infoUI).InitPopUp(infoData);
    }
}
