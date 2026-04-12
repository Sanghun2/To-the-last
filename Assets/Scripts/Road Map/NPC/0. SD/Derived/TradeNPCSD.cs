using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradeNPCSD", menuName = "Scriptable Objects/NPC/TradeNPCSD")]
public class TradeNPCSD : NPCSDBase
{
    public Sprite IconImage => iconImage;
    public IReadOnlyList<TradableItemData> TradeItemList => tradeItemList;

    [SerializeField] Sprite iconImage;
    [SerializeField] List<TradableItemData> tradeItemList = new List<TradableItemData>();
}

[Serializable]
public class TradableItemData
{
    public ItemSD ItemSD => itemSD;
    public int InitAmount => initAmount;

    [SerializeField] ItemSD itemSD;
    [SerializeField] int initAmount;
}