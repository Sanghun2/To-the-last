using UnityEngine;

public class ItemStack 
{
    public ItemSD ItemSD => itemSD;

    [SerializeField] protected ItemSD itemSD;
    [SerializeField] protected int amount;

    public ItemStack(ItemSD itemSD, int amount=1) {
        this.itemSD = itemSD;
        this.amount = amount;
    }
}
