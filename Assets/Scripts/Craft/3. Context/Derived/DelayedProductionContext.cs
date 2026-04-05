using UnityEngine;

public class DelayedProductionContext : ProductionContextBase
{
    public int RequireMinutesToComplete { get; }
    public string ID { get; }
    public int Amount { get; }
    public DelayedProductionContext(string id, int amount, int requireMinutesToComplete) {
        ID = id;
        RequireMinutesToComplete = requireMinutesToComplete;
        Amount = amount;
    }
}
