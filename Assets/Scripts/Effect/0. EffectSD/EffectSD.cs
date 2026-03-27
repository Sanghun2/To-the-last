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
    public enum TargetType {
        None,
        Self,
        ClosestEnemy,
    }
}

public interface IEffect
{
    public void ApplyEffect(Entity caster, Entity target);
}


public abstract class EffectSD : SDBase, IEffect
{
    public Effect.TargetType TargetType => targetType;

    [SerializeField] Effect.TargetType targetType;

    public abstract void ApplyEffect(Entity caster, Entity target);

    protected bool IsValid(Entity caster, Entity target) {
        if (caster == null || target == null) { Debug.Log($"entity null. caster null? {caster == null}, target null? {target == null}"); return false; }

        return true;
    }

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_EffectSD");
    }
}
