using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Effect
{
    public enum OperatorType
    {
        Add,
        Multiply,
    }
    public enum ValueType
    {
        Scala,
        Percent,
    }
    public enum ApplyTarget {
        None,
        Self,
        ClosestEnemy,
    }

    public EffectSD EffectSD => effectSD;
    public float Value => value;

    [SerializeField] EffectSD effectSD;
    [SerializeField] float value;
}

public interface IEffect
{
    public void ApplyEffect(Entity caster, Entity target);
}


public abstract class EffectSD : SDBase
{
    public Effect.ApplyTarget TargetType => targetType;

    [SerializeField] Effect.ApplyTarget targetType;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_EffectSD");
    }
}
