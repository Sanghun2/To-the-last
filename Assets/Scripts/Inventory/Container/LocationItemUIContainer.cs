using UnityEngine;

public sealed class LocationItemUIContainer : ListContainerBase<LocationItemUI>
{
    public override void Clear() {
        InitUI();
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].Return();
            contentList[i].Rect.anchoredPosition = Vector2.zero;
        }
    }
}
