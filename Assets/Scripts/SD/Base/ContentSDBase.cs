using System.Collections.Generic;
using UnityEngine;

public abstract class ContentSDBase : TimeSDBase
{
    public IReadOnlyList<Ingredient> Inputs => inputs;
    public int RequiredLevel => requiredLevel;

    [SerializeField] int requiredLevel;
    [SerializeField] protected Ingredient[] inputs;
}
