using System;
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
    public enum ApplyTarget
    {
        None,
        Self,
        ClosestEnemy,
    }

    public EffectSD EffectSD => effectSD;
    public float Value => value;

    [SerializeField] EffectSD effectSD;
    [SerializeField] float value;
}
