using System;
using BilliotGames;
using UnityEngine;

public interface IPool
{
    public bool IsActive { get; }

    public void Init();
    public void Activate();
    public void Return();
}

public abstract class ContainerBase<TContent> : UIBase, IPrefabContainer<TContent> where TContent : Component, IPool
{
    public string PrefabPath => trimmedPath;
    public GameObject Prefab => prefab;
    public Transform ContainerTr => containerTr;

    [SerializeField] string prefabResourcePath;
    [SerializeField] Transform containerTr;
    [SerializeField] int initialPrefabCount = 10;
    protected GameObject prefab;
    private string trimmedPath;

    public override void InitUI() {
        if (IsInit) return;
        _isInit = true;
        if (prefab == null) {
            trimmedPath = TrimPath(prefabResourcePath);
            prefab = Resources.Load<GameObject>(PrefabPath);
            if (prefab == null) { Debug.LogError($"<color=red>path: {PrefabPath}에 prefab이 존재하지 않음</color>"); return; }
        }

        for (int i = 0; i < initialPrefabCount; i++) {
            var obj = CreateObj();
            if (obj != null) {
                obj.Return();
            }
        }
    }

    private string TrimPath(string prefabPath) {

        prefabPath = prefabPath.Replace(".Prefab", "");
        prefabPath = prefabPath.Replace(".prefab", "");
        int resourcesIndex = prefabPath.IndexOf("Resources/");
        if (resourcesIndex == -1) {
            return prefabPath;
        }

        string path = prefabPath.Substring(resourcesIndex + "Resources/".Length);
        int extensionIndex = path.LastIndexOf('.');
        if (extensionIndex != -1) {
            path = path.Substring(0, extensionIndex);
        }

        return path;
    }

    public virtual TContent CreateObj() {
        return CreateObj(Prefab, ContainerTr);
    }
    public virtual TContent CreateObj(GameObject prefab, Transform parent) {
        InitUI();
        if (prefab == null) { Debug.LogError($"생성하려는 prefab이 없음"); return null; }
        GameObject obj = Instantiate(prefab, parent);
        var content = obj.GetComponentInChildren<TContent>();
        content.Init();
        return content;
    }

    public abstract TContent GetObj();
    public abstract void Clear();

    protected virtual void Reset() {
        if (containerTr == null) {
            containerTr = transform;
        }
    }
}
