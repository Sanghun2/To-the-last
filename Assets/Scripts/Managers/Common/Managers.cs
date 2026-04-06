using System;
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
    public static SelectionManager Select
    {
        get
        {
            if (_selectActionPipeline == null) {
                _selectActionPipeline = new SelectionManager();
            }

            return _selectActionPipeline;
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
    public static ExplorationManager Exploration
    {
        get
        {
            if (_explorationSystem == null) {
                _explorationSystem = new ExplorationManager();
            }

            return _explorationSystem;
        }
    }
    public static TurnManager Turn
    {
        get
        {
            if (_turnManager == null) {
                _turnManager = new TurnManager();
            }

            return _turnManager;
        }
    }
    public static BattleSystem BattleSystem
    {
        get
        {
            if (_battleManager == null) {
                _battleManager = new BattleSystem();
            }

            return _battleManager;
        }
    }
    public static SceneController Scene
    {
        get
        {
            if (_sceneController == null) {
                _sceneController = SceneController.Instance;
                _sceneController.SetSceneTransitor(FindAnyObjectByType<PanelTransitor>());
            }

            return _sceneController;
        }
    }
    public static ProcessManager Process
    {
        get
        {
            if (_processManager == null) {
                _processManager = new ProcessManager();

                //var loginChain = new ProcessChain("log in");
                //var prepareChain = new ProcessChain("prepare");
                    //.AddProcess(new Process());

                //_processManager.RegisterChain(loginChain);
                //_processManager.RegisterChain(prepareChain);
            }

            return _processManager;
        }
    }
    public static EffectManager Effect
    {
        get { 
            if (_effectSystem == null) {
                _effectSystem = new EffectManager();
            }

            return _effectSystem;
        }
    }
    public static TraitManager Trait
    {
        get
        {
            if (_traitManager == null) {
                _traitManager = new TraitManager();
            }

            return _traitManager;
        }
    }
    public static CharacterManager Character
    {
        get
        {
            if (_characterManager == null) {
                _characterManager = new CharacterManager();
            }

            return _characterManager;
        }
    }
    public static GameBootStrapper BootStrap
    {
        get
        {
            if (_bootStrapper == null) {
                _bootStrapper = new GameBootStrapper();
            }

            return _bootStrapper;
        }
    }
    public static EventHistory History
    {
        get
        {
            if (_eventHistory == null) {
                _eventHistory = new EventHistory();
            }

            return _eventHistory;
        }
    }
    public static InventoryManager Inventory
    {
        get
        {
            if (_inventoryManager == null) {
                _inventoryManager = new InventoryManager();
            }

            return _inventoryManager;
        }
    }
    public static ScreenBlockCanvas ScreenBlocker
    {
        get
        {
            if (_screenBlocker == null) {
                _screenBlocker = FindAnyObjectByType<ScreenBlockCanvas>(FindObjectsInactive.Include);
            }

            return _screenBlocker;
        }
    }
    public static UpgradeManager Upgrade
    {
        get
        {
            if (_upgradeManager == null) {
                _upgradeManager = new UpgradeManager();
            }

            return _upgradeManager;
        }
    }
    public static StructureManager Structure
    {
        get
        {
            if (_structureManager == null) {
                _structureManager = new StructureManager();
            }

            return _structureManager;
        }
    }
    public static SoundManager Sound
    {
        get
        {
            if (_soundManager == null) {
                _soundManager = new SoundManager();
                _soundManager.SetSFXSourceContainer(new AudioSourceContainer(), new PooledSourceStrategy(), 10);
                _soundManager.SetBGMSourceContainer(new AudioSourceContainer(), new DedicatedSourceStrategy(), 1);
                _soundManager.SetClipLoader(new ResourceClipLoader("Sound"));
            }

            return _soundManager;
        }
    }


    // InGame
    private static StructureManager _structureManager;
    private static UpgradeManager _upgradeManager;
    private static InventoryManager _inventoryManager;
    private static EffectManager _effectSystem;
    private static BattleSystem _battleManager;
    private static TurnManager _turnManager;
    private static ExplorationManager _explorationSystem;
    private static DialogManager _dialogManager;
    private static SelectionManager _selectActionPipeline;
    private static EncounterManager _encounterManager;
    private static LocationManager _locationManager;
    private static ItemManager _itemManager;
    private static CraftManager _craftManager;
    private static PlayerManager _playerManager;
    private static ConstructionManager constructionManager;
    private static TimeManager _timeManager;
    private static JobHandler jobHandler;
    private static EventHistory _eventHistory;

    // Main Menu
    private static GameBootStrapper _bootStrapper;
    private static CharacterManager _characterManager;

    // Common
    private static ScreenBlockCanvas _screenBlocker;
    private static SoundManager _soundManager;
    private static TraitManager _traitManager;
    private static ProcessManager _processManager;
    private static SceneController _sceneController;
    private static CoroutineManager coroutineManager;
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
        SD.TryRegisterSD(new ActivitySDContainer("SD/Content/Activity"));
        SD.TryRegisterSD(new ProductionSDContainer("SD/Content/Production"));
        SD.TryRegisterSD(new SelectionSDContainer("SD/Selection"));
        SD.TryRegisterSD(new DialogSDContainer("SD/Dialog"));
        SD.TryRegisterSD(new IconSDContainer("SD/Icon"));
        SD.TryRegisterSD(new SkillSDContainer("SD/Skill"));
        SD.TryRegisterSD(new EffectSDContainer("SD/Effect"));
        SD.TryRegisterSD(new AnimationSpriteSDContainer("SD/Animation Sprite"));
        SD.TryRegisterSD(new TraitSDContainer("SD/Trait"));
        SD.TryRegisterSD(new UpgradeSDContainer("SD/Upgrade"));
        SD.TryRegisterSD(new ExpensionSDContainer("SD/Expension"));
        SD.TryRegisterSD(new NPCSDContainer("SD/NPC"));
        //SD.TryRegisterSD(new ExpensionSDContainer("SD/Quest"));


        List<IInitializable> initList = new List<IInitializable>() {
            Process,
            UI.GetUI<TimerUI>(),
            Time,
            Job,
            Structure,
            Construction,
            Player,
            Encounter,
            Location,
            Character,
        };

        for (int i = 0; i < initList.Count; i++) {
            initList[i]?.Init();
        }

        UI.OnUIOpened -= UISound;
        UI.OnUIOpened += UISound;

        UI.OnUIClosed -= UISound;
        UI.OnUIClosed += UISound;
    }

    private void UISound(UIBase @base) {
        Sound.PlaySound("UI Interaction");
    }
}

public interface IInitializable
{
    public bool IsInit { get; }

    void Init();
    void Release();
}
