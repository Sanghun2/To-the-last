using System.Collections.Generic;
using UnityEngine;

public class TradeNPCData : NPCDataBase
{
    public Sprite IconImage { get; }
    public float MaxAffinity { get; }
    public IReadOnlyList<TradableItemData> DefaultItemList { get; }

    public TradeNPCData(string npcID, Sprite iconImage, float maxAffinity, IReadOnlyList<TradableItemData> defaultItemList) : base(npcID) {
        IconImage = iconImage;
        MaxAffinity = maxAffinity;
        DefaultItemList = defaultItemList;
    }
}
