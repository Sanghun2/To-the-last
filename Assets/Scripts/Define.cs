using UnityEngine;

public class Define
{
    public class Path {
        public const string ASSET_LOAD_FOLDER = "Assets/@Resources";
        public const string IMAGE_ASSET_LOAD_PATH = ASSET_LOAD_FOLDER + "/Image";
        public const string ICON_ASSET_LOAD_PATH = IMAGE_ASSET_LOAD_PATH + "/Icon";

    }

    public class Sound {
        public const string COLLECT = "Collect";
    }

    public class Tag {
        public const string PLAYER = "player";
        public const string STORAGE = "storage";

        public const string BASEMENT = "basement"; // id of basement location
    }

    public class Icon {
        public const string UPGRADE_READY = "upgradeReadyIcon";
        public const string PRODUCTION_COMPLETE = "collectReadyIcon";
    }

    public enum FlowType {
        LogIn,
        BootStrapGame,
    }

    public enum EndingType {
        Failed,
        Esacped,
    }

    public enum Stat {
        Hp = 0,
        Hunger = 1,
        Thirst = 2,
        Mental = 3,
        Temperature = 4,

        Strength,
        Agility,
        Focus,
        Toughness,
    }

    public enum StatType {
        Flat,
        Bounded,
        Group,
    }
    public enum StatDetail {
        none,
        current,
        max,
    }

    public enum RequirementType {
        Free,
        Consume,
        Check,
    }
    public enum VitalState
    {
        None,
        Alive,
        Dead,
    }

    public enum BattleState
    {
        Exit,
        Ready,
        Wait,
        Resolve,
        Finish,
    }

    public enum ActionAnimationType
    {
        Default,
        SwingAttack,
        StabAttack,
        Dodge,
        Charge,
    }

    public const string MAINMENU_SCENE_ID = "main";
    public const string INGAME_SCENE_ID = "ingame";
}
