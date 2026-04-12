using UnityEngine;

public class TradeNPCData : NPCDataBase
{
    public Sprite IconImage { get; }

    public TradeNPCData(string npcID, Sprite iconImage) : base(npcID) {
        IconImage = iconImage;
    }
}
