using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ListContainerBase<TContent> : ContainerBase<TContent> where TContent : Component, IPool
{
    public int ContentCount => contentList.Count;

    protected List<TContent> contentList = new List<TContent>();

    public override TContent CreateObj(GameObject prefab, Transform parent) {
        InitUI();
        var newObj = base.CreateObj(prefab, parent);
        contentList.Add(newObj);
        newObj.Activate();
        return newObj;
    }
    public override TContent GetObj() {
        InitUI();
        for (int i = 0; i < contentList.Count; i++) {
            var content = contentList[i];
            if (!content.IsActive) {
                content.Activate();
                return content;
            }
        }

        return CreateObj();
    }

    public virtual TContent GetObjOf(int index) {
        InitUI();
        TContent obj = null;
        if (0 <= index && index < contentList.Count) {
            obj = contentList[index];
            obj.Activate();
            return obj;
        }

        return obj;
    }
    public virtual TContent GetOrCreateObj(int index) {
        InitUI();
        TContent obj = GetObjOf(index);

        if (obj == null) {
            obj = CreateObj();
            obj.Activate();
        }

        return obj;
    }
    public override void Clear() {
        InitUI();
        for (int i = 0; i < contentList.Count; i++) {
            contentList[i].Return();
        }
    }
}
