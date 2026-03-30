using System;
using BilliotGames;
using UnityEngine;

public abstract class EffectSD : SDBase
{
    public Effect.ApplyTarget TargetType => targetType;

    [SerializeField] Effect.ApplyTarget targetType;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_EffectSD");
    }
}
