using BilliotGames;
using UnityEngine;

public sealed class ProductionContextBuilderContainer : TypeRegistry<ProductionDataBase, ProductionContextBuilderBase>
{
    public ProductionContextBuilderContainer() {
        Register<ProductionData>(new ProductionContextBuilder());
        Register<DelayedProductionData>(new DelayedProductionContextBuilder());
    }
}
