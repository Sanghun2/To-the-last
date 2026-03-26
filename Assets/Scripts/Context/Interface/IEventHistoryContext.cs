using UnityEngine;

public interface IEventHistoryContext : IContext
{
    EventHistory EventHistory { get; }
}
