using System;
using BilliotGames;
using UnityEngine;

public class CustomButtonContainer : ListContainerBase<CustomButton>
{
    [SerializeField] int maxButtonCount = 2;

    public override bool TryGetObj(int index, out CustomButton content) {
        if (0 <= index && index < contentList.Count) {
            content = contentList[index];
            return true;
        }

        if (contentList.Count > maxButtonCount) {
            content = CreateObj();
            return true;
        }

        Debug.LogError($"max count ({maxButtonCount}) 보다 많은 버튼을 생성할 수 없음. input index: {index}, current count: {contentList.Count}");
        content = null;
        return false;
    }

    public override CustomButton GetObj() {
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

    internal void Clear() {
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].CloseUI();
        }
    }
}
