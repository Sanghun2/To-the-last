using UnityEngine;

public class ShowInfomationAction : ActionBase<PopUpDataBase>
{
    public ShowInfomationAction(PopUpDataBase infomationData) {
        SetParameter(infomationData);
    }

    public override void Execute() {
        if (parameter == null) { Debug.LogError("보여줄 데이터가 null"); return; } 
        Managers.UI.OpenUI<InfomationPopUpUI>().InitPopUp(parameter);
    }
}
