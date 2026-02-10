using UnityEngine;

public class ShowInfomationAction : ActionBase<InfomationPopUpData>
{
    public ShowInfomationAction(InfomationPopUpData infomationData) {
        SetParameter(infomationData);
    }

    public override void Execute() {
        if (parameter == null) { Debug.LogError("보여줄 데이터가 null"); return; } 
        Managers.UI.OpenUI<InfomationPopUpUI>().InitUI(parameter);
    }
}
