using BilliotGames;
using UnityEngine;

public class ProductionContextProcessor : ProductionContextProcessorBase<ProductionContext>
{
    public override bool TryProcessContext(ProductionContext contentContext, ProductionContentUI targetUI) {
        ItemStack createdItem = Managers.Craft.CreateItem(contentContext.ID, contentContext.Amount);
        if (!Managers.Inventory.TryGetInventoryByTag(Define.Tag.PLAYER, out var inventories)) { return false; }
        if (InventoryUtility.TryPushItem(inventories, createdItem, true)) {
            return true;
        }

        return false;
    }
}
