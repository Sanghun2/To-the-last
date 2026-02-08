using BilliotGames;
using UnityEngine;

public interface IContent
{
    public void Init();
    public void Activate();
    public void Release();
}

public abstract class ContainerBase<TContent> : UIBase, IPrefabContainer<TContent> where TContent : Component, IContent
{
    public string PrefabPath => prefabResourcePath;
    public GameObject Prefab => prefab;
    public Transform ContainerTr => containerTr;

    [SerializeField] string prefabResourcePath;
    [SerializeField] Transform containerTr;
    [SerializeField] int initialPrefabCount = 10;
    protected GameObject prefab;

    public override void InitUI() {
        if (IsInit) return;
        _isInit = true;
        if (prefab == null) {
            prefab = Resources.Load<GameObject>(PrefabPath);
            if (prefab == null) { Debug.LogError($"<color=red>{PrefabPath}에 prefab이 존재하지 않음</color>"); return; }
        }

        for (int i = 0; i < initialPrefabCount; i++) {
            var obj = CreateObj();
            if (obj != null) {
                obj.Init();
                obj.Release();
            }
        }
    }

    public virtual TContent CreateObj() {
        if (!IsInit) InitUI();
        return CreateObj(Prefab, ContainerTr);
    }
    public virtual TContent CreateObj(GameObject prefab, Transform parent) {
        if (!IsInit) InitUI();
        GameObject obj = Instantiate(prefab, parent);
        return obj.GetComponentInChildren<TContent>();
    }

    public abstract TContent GetObj();
}
