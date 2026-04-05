using UnityEngine;

public class ProductionData : ProductionDataBase
{
    public string ID { get; }
    public int Amount { get; }

    public ProductionData(string iD, int amount) {
        ID = iD;
        Amount = amount;
    }
}
