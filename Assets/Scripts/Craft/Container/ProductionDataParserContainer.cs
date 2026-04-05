using BilliotGames;
using UnityEngine;

public sealed class ProductionDataParserContainer : TypeRegistry<ProductionContentSD, ProductionDataParserBase>
{
    public ProductionDataParserContainer() {
        Register<ProductionContentSD>(new ProductionDataParser());
        Register<DelayedProductionContentSD>(new DelayedProductionDataParser());
    }
}
