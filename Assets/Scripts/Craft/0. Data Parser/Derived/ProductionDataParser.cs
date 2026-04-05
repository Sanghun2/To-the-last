using System.Linq;
using UnityEngine;

public class ProductionDataParser : ProductionDataParserBase<ProductionContentSD, ProductionData>
{
    public override ProductionData ParseData(ProductionContentSD contentSD) {
        return new ProductionData(
            contentSD.ID,
            contentSD.Outputs.First()?.Amount ?? 0
            );
    }
}
