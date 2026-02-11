using System.Collections.Generic;
using UnityEngine;

public abstract class ListContainerBase<TContent> : ContainerBase<TContent> where TContent : Component, IContent
{
    public int ContentCount => contentList.Count;

    protected List<TContent> contentList = new List<TContent>();

    public override TContent CreateObj(GameObject prefab, Transform parent) {
        if (!IsInit) InitUI();
        var newObj = base.CreateObj(prefab, parent);
        contentList.Add(newObj);
        newObj.Activate();
        return newObj;
    }

    public abstract bool TryGetObj(int index, out TContent content);
    public virtual void ReleaseContainer() {
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].Release();
        }
    }
}
