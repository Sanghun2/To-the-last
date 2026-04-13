using UnityEngine;

public class TradeNPCData : NPCDataBase
{
    public Sprite IconImage { get; }
    public float MaxAffinity { get; }

    public TradeNPCData(string npcID, Sprite iconImage, float maxAffinity) : base(npcID) {
        IconImage = iconImage;
        MaxAffinity = maxAffinity;
    }
}
