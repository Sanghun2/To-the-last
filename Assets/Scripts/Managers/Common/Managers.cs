using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    public static SDManager SD
    {
        get
        {
            if (_sdManager == null) {
                _sdManager = new SDManager();
            }

            return _sdManager;
        }
    }

    private static SDManager _sdManager;

    //static Managers Instance
    //{
    //    get
    //    {
    //        if (_instance == null) {
    //            _instance = FindAnyObjectByType<Managers>(FindObjectsInactive.Include);
    //        }

    //        return _instance;
    //    }
    //}
    //static Managers _instance;

    private void Awake() {
        SD.TryRegisterSD(new ItemSDContainer("SD/Item"));
    }
}
