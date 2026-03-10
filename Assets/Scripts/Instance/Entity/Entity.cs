using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public abstract class Entity : IReadOnlyEntity
{
    public string EntityID => _entityID;

    protected string _entityID;

    public Entity(string entityID) {
        this._entityID = entityID;
    }
}

public interface IReadOnlyEntity
{
    public string EntityID { get; }
}
