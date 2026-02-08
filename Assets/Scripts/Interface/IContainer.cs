using UnityEngine;

public interface IPrefabContainer<T>
{
    string PrefabPath { get; }
    GameObject Prefab { get; }
    Transform ContainerTr { get; }

    T CreateObj(GameObject prefab, Transform parent);
    T GetObj();
}
