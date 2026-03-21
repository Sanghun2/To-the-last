using UnityEngine;

public class Trait
{
    public TraitData Data => data;

    [SerializeField] TraitData data;

    public Trait(TraitData data) {
        this.data = data;
    }
}
