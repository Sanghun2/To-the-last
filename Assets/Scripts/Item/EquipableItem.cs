using UnityEngine;

public class EquipableItem : ItemStack, IEqpuipable
{
    public EquipableItem(ItemSD itemSD, int amount = 1) : base(itemSD, amount) {
    }

    public void Equip() {
        throw new System.NotImplementedException();
    }
}
