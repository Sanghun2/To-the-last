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
    public MetabolicSystem MetabolicSystem => metabolicSystem;

    public string CurrentLocationID
    {
        get
        {
            Init();
            return currentLocationUID;
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
    public StatContainer StatContainer => playerEntity.Stats;
    public string CharacterID => _characterID;

    public Entity Entity => playerEntity;

    public event Action<Define.VitalState, Define.VitalState> OnVitalStateChanged;

    [SerializeField] SkillContainer skillContainer = new SkillContainer();
    [SerializeField] private string currentLocationUID;
    private bool _isInit;
    private Entity playerEntity = new Entity("player", new StatContainer());
    private MetabolicSystem metabolicSystem = new MetabolicSystem();
    private InventoryBase _inventory = new SimpleInventory("player inventory", 100);
    private Define.VitalState vitalState;
    private HashSet<string> traitSet = new HashSet<string>();
    private int traitPoint;
    private string _characterID;

    public void Init() {
        if (IsInit) return;

        SetAsDefaultTestStats();
        SetAsDefaultTestMetablism();

        ResetSkills();
        SetAsDefaultLocation();

        Managers.Time.OnTimeChanged -= ConsumeStatAdaptor;
        Managers.Time.OnTimeChanged += ConsumeStatAdaptor;

        RegisterEvent(OnPlayerDead, Define.Stat.Hp, Define.StatDetail.current);

        _isInit = true;
    }


    public void Release() {
        skillContainer.Release();
    }


    #region Stat

    public void RegisterEvent(Action<Value<float>> @event, Define.Stat targetStat, Define.StatDetail detail=Define.StatDetail.none) {
        StatContainer.RegisterEvent(@event, targetStat.ToID(), detail.ToString());
    }
    public void UnregisterEvent(Action<Value<float>> @event, Define.Stat targetStat, Define.StatDetail detail=Define.StatDetail.none) {
        StatContainer.UnregisterEvent(@event, targetStat.ToID(), detail.ToString());
    }

    public Value<float>? GetStatValue(Define.Stat targetStat) {
        return StatContainer.GetRawValue(targetStat.ToID());
    }
    public void ChangeStat(Define.Stat targetStat, float deltaValue) {
        StatContainer.TryChangeRawValue(targetStat.ToID(), deltaValue);
    }
    public void ChangeMaxStat(Define.Stat targetStat, float deltaValue) {
        StatContainer.TryChangeRawMaxVale(targetStat.ToID(), deltaValue);
    }

    #endregion

    #region Location

    public void SetCurrentLocation(string locationUID) {
        currentLocationUID = locationUID;
    }
    public void SetCurrentLocation(LocationBase currentLocation, LocationBase prevLocation) {
        SetCurrentLocation(currentLocation.Data);
    }
    public void SetCurrentLocation(LocationData locationData) {
        SetCurrentLocation(locationData.LocationUID);
    }
    public void SetAsDefaultLocation() {
        if (string.IsNullOrEmpty(currentLocationUID)) {
            currentLocationUID = LocationUtility.basementSDID;
        }
    }

    #endregion

    #region Skill

    public void RegisterSkill(int index, SkillData skillData) {
        skillContainer.RegisterSkill(index, skillData);
    }
    public void ClearSkill(int index) {
        skillContainer.ClearSkill(index);
    }

    #endregion

    #region Trait

    public void SetTraits(IReadOnlyList<Trait> selectedTraits) {
        traitSet.Clear();
        for (int i = 0; i < selectedTraits.Count; i++) {
            Trait trait = selectedTraits[i];
            traitSet.Add(trait.Data.ID);
        }

        //Debug.Log($"trait set. total count? {traitSet.Count}");
    }
    public int GetAvailableTraitPoint() {
        return traitPoint == 0 ? 5 : traitPoint;
    }

    #endregion

    #region Chracter

    public void SetCharacter(string characterID) {
        this._characterID = characterID;
    }

    #endregion


    // private
    private void RegisterStat(IStatEntry stat) {
        StatContainer.RegisterStat(stat);
    }
    private void ResetSkills() {
        skillContainer.Init();
    }

    private void SetAsDefaultTestMetablism() {
        List<(Define.Stat stat, float value)> consumValues = new() {
            (Define.Stat.Hunger, 0.1f),
            (Define.Stat.Thirst, 0.1f),
        };

        metabolicSystem.InitMetabolism(consumValues);
    }
    private void SetAsDefaultTestStats() {
        RegisterStat(StatUtility.CreateStatGroup(Define.Stat.Hp, 100));
        RegisterStat(StatUtility.CreateStatGroup(Define.Stat.Hunger, 100));
        RegisterStat(StatUtility.CreateStatGroup(Define.Stat.Thirst, 100));
        RegisterStat(StatUtility.CreateStatGroup(Define.Stat.Mental, 100));
        RegisterStat(StatUtility.CreateStat(Define.Stat.Temperature, 36.5f));

        RegisterStat(StatUtility.CreateStat(Define.Stat.Strength, 20));
        RegisterStat(StatUtility.CreateStat(Define.Stat.Agility, 10));
        RegisterStat(StatUtility.CreateStat(Define.Stat.Toughness, 10));
        RegisterStat(StatUtility.CreateStat(Define.Stat.Focus, 20));
    }

    private void ConsumeStatAdaptor(int day, int hour, int minute, int deltaMinutes) {
        metabolicSystem.ConsumeStats(StatContainer, deltaMinutes);
    }
    private void OnPlayerDead(Value<float> value) {
        if (value.CurrentValue <= 0) {
            VitalState = Define.VitalState.Dead;
        }
    }
}
