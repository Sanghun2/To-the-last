using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class Player : IInitializable
{
    public InventoryBase Inventory => _inventory;

    public bool IsInit => _isInit;

    private bool _isInit;
    private StatContainer statContainer = new StatContainer();
    private InventoryBase _inventory = new SimpleInventory("player inventory", 100);

    public void Init() {
        if (IsInit) return;

        RegisterStat(Define.Stat.Hp, new BoundedStat(100));
        RegisterStat(Define.Stat.Hungriness, new BoundedStat(100));
        RegisterStat(Define.Stat.Thirst, new BoundedStat(100));
        RegisterStat(Define.Stat.Mental, new BoundedStat(100));
        RegisterStat(Define.Stat.Temperture, new Stat());

        _isInit = true;
    }
    public void Release() {

    }

    public void RegisterEvent(Define.Stat targetStat, Action<Value> @event) {
        statContainer.RegisterEvent(targetStat.ToID(), @event);
    }
    public void UnregisterEvent(Define.Stat targetStat, Action<Value> @event) {
        statContainer.UnregisterEvent(targetStat.ToID(), @event);
    }

    public Value? GetStatValue(Define.Stat targetStat) {
        return statContainer.GetStatValue(targetStat.ToID());
    }
    public void ChangeStat(Define.Stat targetStat, float deltaValue) {
        statContainer.ChangeStat(targetStat.ToID(), deltaValue);
    }

    private void RegisterStat(Define.Stat statType, Stat stat) {
        statContainer.RegisterStat(statType.ToID(), stat);
    }
}
