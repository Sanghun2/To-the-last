using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

public static partial class Extension
{
    public static ItemData ToItemData(this ItemSD itemSD) {
        return new ItemData(itemSD.ID, itemSD.MaxStackCount);
    }

    public static string ToID(this Define.Stat statType) {
        return statType.ToString();
    }
}
