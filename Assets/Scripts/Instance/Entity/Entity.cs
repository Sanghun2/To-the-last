using System;
using UnityEngine;

[Serializable]
public abstract class Entity : IReadOnlyEntity
{
    public string EntityID => entityID;

    protected string entityID;

    public Entity(string entityID) {
        this.entityID = entityID;
    }
}

public interface IReadOnlyEntity
{
    public string EntityID { get; }
}
