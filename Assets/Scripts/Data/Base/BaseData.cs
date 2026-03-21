using UnityEngine;

public class BaseData
{
    public string ID => id;

    [SerializeField] protected string id;

    public BaseData(string id) {
        this.id = id;
    }
}
