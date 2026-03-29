using BilliotGames;
using UnityEngine;

public class ExtendedItemData : ItemData
{
    public int Weight => weight;

    private int weight;

    public ExtendedItemData(string itemID, int maxStackAmount) : base(itemID, maxStackAmount) {

    }

    public ExtendedItemData(string itemID, int maxAmount, int weight) : this(itemID, maxAmount) {
        this.weight = weight;
    }
}
