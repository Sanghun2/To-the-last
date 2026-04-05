using UnityEngine;

public class ProductionContext : ProductionContextBase
{
    public string ID { get; }
    public int Amount { get; }

    public ProductionContext(string id, int amount) {
        ID = id;
        Amount = amount;
    }
}
