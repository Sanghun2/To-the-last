using UnityEngine;

public class DataBase
{
    public string ID => id;

    [SerializeField] protected string id;

    public DataBase(string id) {
        this.id = id;
    }
}
