using BilliotGames;
using Unity.VisualScripting;
using UnityEngine;

public static class Extension
{
    public static ItemData ToItemData(this ItemSD itemSD) {
        return new ItemData(itemSD.ID, itemSD.MaxStackCount);
    }
}
