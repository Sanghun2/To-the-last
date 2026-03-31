using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Entity : IReadOnlyEntity
{
    public string EntityID => _entityID;
    public StatContainer Stats => _statContainer;

    protected string _entityID;
    protected StatContainer _statContainer;

    public Entity(string entityID, StatContainer statContainer=null) {
        this._entityID = entityID;
        _statContainer = statContainer;
    }
}

public interface IReadOnlyEntity
{
    public string EntityID { get; }
}
