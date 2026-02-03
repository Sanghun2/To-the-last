using UnityEngine;

public class UsableItem : ItemStack, IUsable
{
    public UsableItem(ItemSD itemSD, int amount = 1) : base(itemSD, amount) {
    }

    public void Use() {
        throw new System.NotImplementedException();
    }
}
