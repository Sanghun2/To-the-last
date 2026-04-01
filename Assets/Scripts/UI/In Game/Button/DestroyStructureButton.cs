using BilliotGames;
using UnityEngine;

public class DestroyStructureButton : ButtonBase
{
    protected override void ButtonAction() {
        var popUpData = new InfomationPopUpData(
            "정말 철거하시겠습니까?", 
            "철거를 하면 일정 부분의 재료를 회수합니다.", 
            new ActionData[] {
            new ActionData("취소", () => Managers.UI.CloseUI<InfomationPopUpUI>()),
            new ActionData($"철거({Managers.Structure.GetStructure(Managers.Construction.CurrentLocationIndex)?.StructureContext.ConstructionTime})분", () => Managers.Construction.DestroyCurrentStructure())
        });
        Managers.UI.OpenUI<InfomationPopUpUI>().InitPopUp(popUpData);
    }
}
