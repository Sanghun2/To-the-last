using BilliotGames;
using UnityEngine;

public sealed class ProductionContextProcessorContainer : TypeRegistry<ProductionContextBase, ProductionContextProcessorBase>
{
    public ProductionContextProcessorContainer() {
        Register<ProductionContext>(new ProductionContextProcessor());
        Register<DelayedProductionContext>(new DelayedProductionContextProcessor());
    }
}
