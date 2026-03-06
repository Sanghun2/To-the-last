using UnityEngine;

public class Define
{
    public enum Stat {
        Hp = 0,
        Hungriness = 1,
        Thirst = 2,
        Mental = 3,
        Temperture = 4,

        Strength,
        Agility,
        Focus,
        Toughness,
    }

    public enum RequirementType {
        Free,
        Consume,
        Check,
    }

    public enum BattleState
    {
        None,
        Ready,
        Wait,
        Resolve,
        Finish,
    }

    //public enum SelectionType {
    //    Free,
    //    Loot,
    //    Battle,
    //    Talk,
    //}
}
