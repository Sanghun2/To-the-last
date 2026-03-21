using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class EffectHandlerAttribute : Attribute
{
    public string Key { get; }

    public EffectHandlerAttribute(string key) {
        Key = key;
    }
}
