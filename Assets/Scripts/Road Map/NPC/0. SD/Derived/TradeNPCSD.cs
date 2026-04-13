using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradeNPCSD", menuName = "Scriptable Objects/NPC/TradeNPCSD")]
public class TradeNPCSD : NPCSDBase
{
    public IReadOnlyList<TradableItemData> TradeItemList => tradeItemList;
    public float MaxAffinity => maxAffinity;

    [SerializeField] float maxAffinity = 10;
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