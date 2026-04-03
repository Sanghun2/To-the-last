using UnityEngine;

public class ShowInfomationAction : ActionBase<PopUpDataBase>
{
    private bool executing;

    public ShowInfomationAction(PopUpDataBase infomationData) {
        SetParameter(infomationData);
    }

    public override void Execute() {
        if (parameter == null) { Debug.LogError("보여줄 데이터가 null"); return; }
        if (executing) return;

        executing = true;
        var ui = Managers.UI.GetUI<InfomationPopUpUI>();
        ui.InitPopUp(parameter);
        Managers.UI.OpenUI(ui);
        executing = false;
    }
}
