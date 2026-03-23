using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class TraitManager
{
    public int AvailableTraitPoint
    {
        get => _availableTraitPoint;
        set
        {
            _availableTraitPoint = value;
            OnTraitPointChanged?.Invoke(_availableTraitPoint);
            Debug.Log($"point changed: {_availableTraitPoint}");
        }
    }

    private int _availableTraitPoint;
    private Dictionary<string, Trait> selectedTraitDict = new();

    public event Action<int> OnTraitPointChanged;
    public event Action<Trait> OnTraitSelected;
    public event Action<Trait> OnTraitUnselected;


    public void InitTraitData() {
        InitTraitPoint();
        InitSelectedTraits();
    }


    public void ChangeTraitPoint(int point) {
        AvailableTraitPoint += point;
    }


    public void SelectTrait(Trait trait) {
        if (selectedTraitDict.TryAdd(trait.Data.ID, trait)) {
            OnTraitSelected?.Invoke(trait);
        }

        Debug.LogError($"<color=red>trait repeated. id? {trait.Data.ID}</color>");
    }
    public void UnselectTrait(Trait trait) {
        string traitID = trait.Data.ID;
        if (selectedTraitDict.ContainsKey(traitID)) {
            selectedTraitDict.Remove(traitID);
            OnTraitUnselected?.Invoke(trait);
        }
    }
    public IReadOnlyList<Trait> GetSelectedTraits() {
        List<Trait> selectedList = new List<Trait>(selectedTraitDict.Count);
        foreach (var trait in selectedTraitDict.Values) {
            selectedList.Add(trait);
        }

        return selectedList;
    }


    private void InitSelectedTraits() {
        selectedTraitDict.Clear();
    }
    private void InitTraitPoint() {
        AvailableTraitPoint = Managers.Player.PlayerData.GetAvailableTraitPoint();
    }
}
