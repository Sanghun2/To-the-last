using BilliotGames;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] ItemSD[] itemLists;
    [SerializeField] int createAmount;

    private ItemMoveProcessorBase collector = new SimpleItemMoveProcessor();
    [SerializeField] InventoryUIBase targetInvenUI;
    [SerializeField] InventoryUIBase playerInvenUI;
    private InventoryBase targetInven;
    private InventoryBase playerInven;

    public void CreateRandomItems() {
        targetInven = targetInvenUI.Inventory;
        playerInven = playerInvenUI.Inventory;

        for (int i = 0; i < createAmount; i++) {
            var item = itemLists[Random.Range(0, itemLists.Length)];
            var amount = Random.Range(1, 10);
            targetInven.TryPushItem(new ItemStack(item.ToData(), amount), out var overflow);
        }

        targetInvenUI.InitInventory(targetInven).ShowInventory();
        playerInvenUI.InitInventory(playerInven).ShowInventory();
    }
    public void CollectAllItems() {

        if (Managers.Inventory.TryGetInventoryByID("player", out var playerInven) == false) { return; }

        playerInvenUI.InitInventory(playerInven);
        (playerInven as SimpleInventory)?.SetWeightCounter(50);

        collector.MoveAllItems(targetInven, playerInven);

        targetInvenUI.ShowInventory(targetInven);
        playerInvenUI.ShowInventory(playerInven);
    }
}
