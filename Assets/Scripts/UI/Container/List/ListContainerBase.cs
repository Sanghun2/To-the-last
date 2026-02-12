using System.Collections.Generic;
using UnityEngine;

public abstract class ListContainerBase<TContent> : ContainerBase<TContent> where TContent : Component, IPool
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
    public override TContent GetObj() {
        for (int i = 0; i < contentList.Count; i++) {
            var content = contentList[i];
            if (!content.IsActive) {
                content.Activate();
                return content;
            }
        }

        return CreateObj();
    }

    public virtual TContent GetObj(int index) {
        TContent obj = null;
        if (0 <= index && index < contentList.Count) {
            obj = contentList[index];
            obj.Activate();
            return obj;
        }

        obj = CreateObj();
        obj.Activate();
        return obj;
    }
    public virtual void ReleaseContainer() {
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].Release();
        }
    }
}
