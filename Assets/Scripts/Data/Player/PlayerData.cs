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

    public string CurrentLocationID
    {
        get
        {
            Init();
            return currentLocationID;
        }
    }
    public Define.VitalState VitalState
    {
        get => vitalState;
        set
        {
            var prevState = vitalState;
            vitalState = value;
            if (vitalState != prevState) {
                OnVitalStateChanged?.Invoke(vitalState, prevState);
            }
        }
    }
    public bool IsInit => _isInit;


    public IReadOnlyList<SkillData> SkillList => skillContainer.SkillList;
    public StatContainer StatContainer => statContainer;

    public event Action<Define.VitalState, Define.VitalState> OnVitalStateChanged;

    [SerializeField] SkillContainer skillContainer = new SkillContainer();
    [SerializeField] private string currentLocationID;
    private bool _isInit;
    private StatContainer statContainer = new StatContainer();
    private MetabolicSystem metabolicSystem = new MetabolicSystem();
    private InventoryBase _inventory = new SimpleInventory("player inventory", 100);
    private Define.VitalState vitalState;
    private HashSet<string> traitSet = new HashSet<string>();
    private int traitPoint;

    public void Init() {
        if (IsInit) return;

        SetAsDefaultStats();
        SetAsDefaultMetablism();
        SetAsDefaultLocation();
        ResetSkills();

        Managers.Time.OnTimeChanged -= ConsumeStatAdaptor;
        Managers.Time.OnTimeChanged += ConsumeStatAdaptor;

        RegisterEvent(Define.Stat.Hp, OnPlayerDead);

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
        return statContainer.GetStatRawValue(targetStat.ToID());
    }
    public void ChangeStat(Define.Stat targetStat, float deltaValue) {
        statContainer.TryChangeRawStat(targetStat.ToID(), deltaValue);
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

    public void SetTraits(IReadOnlyList<Trait> selectedTraits) {
        traitSet.Clear();
        for (int i = 0; i < selectedTraits.Count; i++) {
            Trait trait = selectedTraits[i];
            traitSet.Add(trait.Data.ID);
        }

        Debug.Log($"trait set. total count? {traitSet.Count}");
    }

    public int GetAvailableTraitPoint() {
        return traitPoint == 0 ? 5 : traitPoint;
    }

    private void RegisterStat(Define.Stat statType, Stat stat) {
        statContainer.RegisterStat(statType.ToID(), stat);
    }

    private void ResetSkills() {
        skillContainer.Init();
    }
    private void SetAsDefaultLocation() {
        if (string.IsNullOrEmpty(currentLocationID)) {
            currentLocationID = LocationUtility.basementSDID;
        }
    }
    private void ConsumeStatAdaptor(int day, int hour, int minute, int deltaMinutes) {
        metabolicSystem.ConsumeStats(statContainer, deltaMinutes);
    }
    private void SetAsDefaultMetablism() {
        List<(Define.Stat stat, float value)> consumValues = new() {
            (Define.Stat.Hunger, 0.1f),
            (Define.Stat.Thirst, 0.1f),
        };

        metabolicSystem.InitMetabolism(consumValues);
    }
    private void SetAsDefaultStats() {
        RegisterStat(Define.Stat.Hp, new BoundedStat(100));
        RegisterStat(Define.Stat.Hunger, new BoundedStat(100));
        RegisterStat(Define.Stat.Thirst, new BoundedStat(100));
        RegisterStat(Define.Stat.Mental, new BoundedStat(100));
        RegisterStat(Define.Stat.Temperature, new Stat(36.5f));

        RegisterStat(Define.Stat.Strength, new Stat(20));
        RegisterStat(Define.Stat.Agility, new Stat(10));
        RegisterStat(Define.Stat.Toughness, new Stat(10));
        RegisterStat(Define.Stat.Focus, new Stat(20));
    }
    private void OnPlayerDead(Value<float> value) {
        if (value.CurrentValue <= 0) {
            VitalState = Define.VitalState.Dead;
        }
    }
}
