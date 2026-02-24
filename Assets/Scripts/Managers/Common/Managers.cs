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
    public static ConstructionManager Construction
    {
        get
        {
            if (constructionManager == null) {
                constructionManager = new ConstructionManager();
            }

            return constructionManager;
        }
    }
    public static PlayerManager Player
    {
        get
        {
            if (_playerManager == null) {
                _playerManager = new PlayerManager();
            }

            return _playerManager;
        }
    }
    public static CraftManager Craft
    {
        get
        {
            if (_craftManager == null) {
                _craftManager = new CraftManager();
            }

            return _craftManager;
        }
    }
    public static ItemManager Item
    {
        get
        {
            if (_itemManager == null) {
                _itemManager = new ItemManager();
            }

            return _itemManager;
        }
    }
    public static LocationManager Location
    {
        get
        {
            if (_locationManager == null) {
                _locationManager = new LocationManager();
            }

            return _locationManager;
        }
    }
    public static EncounterManager Encounter
    {
        get
        {
            if (_encounterManager == null) {
                _encounterManager = new EncounterManager();
            }

            return _encounterManager;
        }
    }
    public static SelectionSystem SelectionSystem
    {
        get
        {
            if (_selectionSystem == null) {
                _selectionSystem = new SelectionSystem();
            }

            return _selectionSystem;
        }
    }
    public static DialogManager Dialog
    {
        get
        {
            if (_dialogManager == null) {
                _dialogManager = new DialogManager();
                _dialogManager.Init();
            }

            return _dialogManager;
        }
    }
    public static ExplorationSystem Exploration
    {
        get
        {
            if (_explorationSystem == null) {
                _explorationSystem = new ExplorationSystem();
            }

            return _explorationSystem;
        }
    }
    public static ActionCreator ActionCreator
    {
        get
        {
            if (_actionCreator == null) {
                _actionCreator = new ActionCreator();
            }

            return _actionCreator;
        }
    }

    private static ActionCreator _actionCreator;
    private static ExplorationSystem _explorationSystem;
    private static DialogManager _dialogManager;
    private static SelectionSystem _selectionSystem;
    private static EncounterManager _encounterManager;
    private static LocationManager _locationManager;
    private static ItemManager _itemManager;
    private static CraftManager _craftManager;
    private static PlayerManager _playerManager;
    private static ConstructionManager constructionManager;
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
        SD.TryRegisterSD(new CharacterSDContiner("SD/Character"));
        SD.TryRegisterSD(new EncounterSDContainer("SD/Encounter"));
        SD.TryRegisterSD(new ItemSDContainer("SD/Item"));
        SD.TryRegisterSD(new LocationSDContainer("SD/Location"));
        SD.TryRegisterSD(new StructureSDContainer("SD/Structure"));
        SD.TryRegisterSD(new RecipeSDContainer("SD/Recipe"));
        SD.TryRegisterSD(new SelectionSDContainer("SD/Selection"));
        SD.TryRegisterSD(new DialogSDContainer("SD/Dialog"));


        List<IInitializable> initList = new List<IInitializable>() {
            UI.GetUI<TimerUI>(),
            Time,
            Job,
            Construction,
            Player,
            Encounter,
            Location,
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
