using UnityEngine;

public class ProductionContextBuilder : ProductionContextBuilderBase<ProductionData, ProductionContext>
{
    public override ProductionContext BuildContext(ProductionData contentData) {
        return new ProductionContext(
            contentData.ID,
            contentData.Amount
            );
    }
}
