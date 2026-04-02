using System.Collections.Generic;
using UnityEngine;

public interface IRequirementContent
{
    public IReadOnlyList<Ingredient> Requirements { get; }
}
