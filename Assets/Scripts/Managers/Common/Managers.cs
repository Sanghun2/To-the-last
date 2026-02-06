using System.Collections.Generic;
using BilliotGames;
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

    public static UIManager UI
    {
        get
        {
            if (_uiManager == null) {
                _uiManager = new UIManager();
            }

            return _uiManager;
        }
    }

    public static TimeManager Time
    {
        get
        {
            if (_timeManager == null) {
                _timeManager = FindAnyObjectByType<TimeManager>(FindObjectsInactive.Include);
                if (_timeManager.IsInit == false) _timeManager.Init();
               if (_timeManager == null) Debug.LogError($"timer를 찾을 수 없음");
            }

            return _timeManager;
        }
    }
    public static CoroutineManager Coroutine
    {
        get
        {
            if (coroutineManager == null) {
                coroutineManager = FindAnyObjectByType<CoroutineManager>();
            }

            return coroutineManager;
        }
    }
    public static JobHandler Job
    {
        get
        {
            if (jobHandler == null) {
                jobHandler = new JobHandler();
            }

            return jobHandler;
        }
    }

    private static JobHandler jobHandler;
    private static CoroutineManager coroutineManager;
    private static TimeManager _timeManager;
    private static UIManager _uiManager;
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

        List<IInitializable> initList = new List<IInitializable>() {
            UI.GetUI<TimerUI>(),
            Time,
            Job,
        };

        for (int i = 0; i < initList.Count; i++) {
            initList[i].Init();
        }
    }
}

public interface IInitializable
{
    public bool IsInit { get; }

    void Init();
    void Release();
}
