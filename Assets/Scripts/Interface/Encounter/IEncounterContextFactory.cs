using System;
using UnityEngine;

public interface IEncounterContextFactory
{
    public Type TargetSDType { get; }
    public EncounterContext CreateContext(EncounterSD sd);
}
