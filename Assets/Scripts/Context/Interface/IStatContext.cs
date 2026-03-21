using BilliotGames;
using UnityEngine;

public interface IStatContext : IContext
{
    StatContainer Stats { get; }
    Define.Stat TargetStat { get; }
    Effect.OperatorType OperatorType { get; }
    float ModifyingValue { get; }
}
