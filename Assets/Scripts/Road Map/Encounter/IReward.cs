using System.Collections.Generic;
using UnityEngine;

public interface IReward
{
    IReadOnlyList<DropInfo> Rewards { get; }
}
