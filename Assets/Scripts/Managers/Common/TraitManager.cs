using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TraitManager
{
    public int RemainTraitPoint
    {
        get => _remainTraitPoint;
        set
        {
            _remainTraitPoint = value;
            OnTraitPointChanged?.Invoke(_remainTraitPoint);
            //Debug.Log($"point changed: {_remainTraitPoint}");
        }
    }

    private int _remainTraitPoint;
    private Dictionary<string, Trait> selectedTraitDict = new();

    public event Action<int> OnTraitPointChanged;
    public event Action<Trait> OnTraitSelected;
    public event Action<Trait> OnTraitUnselected;
    public event Action<IReadOnlyList<Trait>> OnTraitListInit;


    public void InitTraitDataFromPlayerData() {
        InitTraitPoint();
        InitSelectedTraits();
        InitTraitList();
    }

    private void InitTraitList() {
        if (!Managers.SD.TryGetContainer<TraitSD>(out var traitSDContainer)) return;

        //여기서 linq로 tolist하기 전에 linq에서 sort하는게 더 빠를까? 아니면 tolist를 하고 나서 sort하는게 더 빠를까
        var traitList = traitSDContainer.SDDict.Values.Select(sd => new Trait(sd.ToData())).ToList();
        traitList.Sort((x, y) => x.Data.ID.CompareTo(y.Data.ID));

        OnTraitListInit?.Invoke(traitList);
    }
    public void ResetTraits() {
        OnTraitPointChanged = null;
        OnTraitSelected = null;
        OnTraitUnselected = null;
        OnTraitListInit = null;

        selectedTraitDict.Clear();
        _remainTraitPoint = 0;
    }

    public void ChangeTraitPoint(int point) {
        RemainTraitPoint += point;
    }


    public void SelectTrait(Trait trait) {
        if (selectedTraitDict.TryAdd(trait.Data.ID, trait)) {
            OnTraitSelected?.Invoke(trait);
            return;
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
        RemainTraitPoint = Managers.Player.PlayerData.GetAvailableTraitPoint();
    }
}
