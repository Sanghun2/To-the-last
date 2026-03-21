using System;
using BilliotGames;
using UnityEngine;

public class CustomButtonContainer : ListContainerBase<CustomButton>
{
    [SerializeField] int maxButtonCount = 2;

    public override CustomButton GetOrCreateObj(int index) {
        InitUI();
        CustomButton content = null;
        if (0 <= index && index < contentList.Count) {
            content = contentList[index];
            content.Activate();
            return content;
        }

        if (contentList.Count > maxButtonCount) {
            content = CreateObj();
            content.Activate();
            return content;
        }

        Debug.LogError($"max count ({maxButtonCount}) 보다 많은 버튼을 생성할 수 없음. input index: {index}, current count: {contentList.Count}");
        content = null;
        return content;
    }

    public override CustomButton GetObj() {
        InitUI();
        for (int i = 0; i < contentList.Count; i++) {
            var content = contentList[i];
            if (content.IsActive == false) {
                return content;
            }
        }

        if (contentList.Count < 2) {
            return CreateObj();
        }

        return null;
    }

    public void InitButtons(ActionData[] actions) {
        if (actions.Length > maxButtonCount) { Debug.LogError($"not enough button count"); return; }

        Clear();
        for (int i = 0; i < actions.Length; i++) {
            var button = GetOrCreateObj(i);
            button.InitButton(actions[i]);
        }
    }

    internal void Clear() {
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].CloseUI();
        }
    }
}
