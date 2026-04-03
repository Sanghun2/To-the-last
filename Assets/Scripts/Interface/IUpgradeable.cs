using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
    IReadOnlyList<Ingredient> Requirements { get; }
}
