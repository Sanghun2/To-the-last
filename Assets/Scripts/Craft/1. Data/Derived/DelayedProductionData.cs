using UnityEngine;

public class DelayedProductionData : ProductionDataBase
{
    public string ID { get; }
    public int RequireMinutesToComplete { get; }
    public int Amount { get; }

    public DelayedProductionData(string iD, int amount, int requireMinutesToComplete) {
        ID = iD;
        Amount = amount;
        RequireMinutesToComplete = requireMinutesToComplete;
    }
}
