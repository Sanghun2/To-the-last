using UnityEngine;

public abstract class EffectDataBase
{
    public float Value => value;

    protected float value;

    public void SetValue(float value) => this.value = value;
}
