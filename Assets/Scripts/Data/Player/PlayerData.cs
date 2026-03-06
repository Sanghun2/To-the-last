using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public sealed class PlayerData : IInitializable
{ 
    public InventoryBase Inventory
    {
        get
        {
            Init();
            return _inventory;
        }
    }

    public bool IsInit => _isInit;

    public string CurrentLocationID
    {
        get
        {
            Init();
            return currentLocationID;
        }
    }

    public IReadOnlyList<SkillData> SkillList => skillContainer.SkillList; 

    private bool _isInit;
    private StatContainer statContainer = new StatContainer();
    private InventoryBase _inventory = new SimpleInventory("player inventory", 100);
    [SerializeField] private string currentLocationID;
    [SerializeField] SkillContainer skillContainer = new SkillContainer();

    public void Init() {
        if (IsInit) return;

        RegisterStat(Define.Stat.Hp, new BoundedStat(100));
        RegisterStat(Define.Stat.Hungriness, new BoundedStat(100));
        RegisterStat(Define.Stat.Thirst, new BoundedStat(100));
        RegisterStat(Define.Stat.Mental, new BoundedStat(100));
        RegisterStat(Define.Stat.Temperture, new Stat(36.5f));

        if (string.IsNullOrEmpty(currentLocationID)) {
            currentLocationID = LocationUtility.basementSDID;
        }

        skillContainer.Init();

        _isInit = true;
    }
    public void Release() {
        skillContainer.Release();
    }

    public void RegisterEvent(Define.Stat targetStat, Action<Value<float>> @event) {
        statContainer.RegisterEvent(targetStat.ToID(), @event);
    }
    public void UnregisterEvent(Define.Stat targetStat, Action<Value<float>> @event) {
        statContainer.UnregisterEvent(targetStat.ToID(), @event);
    }

    public Value<float>? GetStatValue(Define.Stat targetStat) {
        return statContainer.GetStatValue(targetStat.ToID());
    }
    public void ChangeStat(Define.Stat targetStat, float deltaValue) {
        statContainer.ChangeStat(targetStat.ToID(), deltaValue);
    }
    public void SetCurrentLocation(string locationID) {
        currentLocationID = locationID;
    }
    public void SetCurrentLocation(LocationSD locationSD) {
        SetCurrentLocation(locationSD.ID);
    }

    public void RegisterSkill(int index, SkillData skillData) {
        skillContainer.RegisterSkill(index, skillData);
    }
    public void ClearSkill(int index) {
        skillContainer.ClearSkill(index);
    }

    private void RegisterStat(Define.Stat statType, Stat stat) {
        statContainer.RegisterStat(statType.ToID(), stat);
    }

}
