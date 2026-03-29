using BilliotGames;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] ItemSD[] itemLists;
    [SerializeField] int createAmount;

    [SerializeField] InventoryBase targetInven;
    private ItemCollectProcessorBase collector = new SimpleItemCollectProcessor();
    [SerializeField] InventoryUIBase targetInvenUI;
    [SerializeField] InventoryUIBase playerInvenUI;

    public void CreateRandomItems() {
        Init();
        Managers.UI.OpenUI<ExplorationUI>();
        for (int i = 0; i < createAmount; i++) {
            var item = itemLists[Random.Range(0, itemLists.Length)];
            var amount = Random.Range(1, 10);
            targetInven.TryPushItem(new ItemStack(item.ToData(), amount), out var overflow);
        }

        targetInvenUI.InitInventory(targetInven).ShowInventory();
    }
    public void CollectAllItems() {

        if (Managers.Inventory.TryGetInventory("player", out var playerInven) == false) { return; }

        playerInvenUI.InitInventory(playerInven);
        var counter = new WeightCounter(50);
        (playerInven as SimpleInventory)?.SetWeightCounter(counter);

        collector.CollectAllItems(targetInven, playerInven);

        targetInvenUI.ShowInventory(targetInven);
        playerInvenUI.ShowInventory(playerInven);
    }

    private void Init() {
        targetInven = new SimpleInventory("location");

        targetInvenUI.InitInventory(targetInven);
    }
}
