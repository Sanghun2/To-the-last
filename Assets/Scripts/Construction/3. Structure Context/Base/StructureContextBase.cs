using System;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public abstract class StructureContextBase
{
    public StructureDataBase Data => _data;
    public string ID => _data.ID;
    public int ConstructionTime => _data.ConstructionTime;
    public Sprite StructureImage => _data.StructureImage;
    public string DisplayText => _data.DisplayText;

    public string CategoryID => _data.CategoryID;

    public IReadOnlyList<Ingredient> Requirements => _data.RequirementItems;
    public Structure.ProcessState ProcessState
    {
        get => _processState;
        set
        {
            var prevState = _processState;
            _processState = value;
            if (_processState != prevState) {
                OnProcessStateChanged?.Invoke(_processState, prevState);
            }
        }
    }


    protected StructureDataBase _data;
    protected Structure.ProcessState _processState;

    public StructureContextBase(StructureDataBase data) {
        this._data = data;
    }

    public event Action<Structure.ProcessState, Structure.ProcessState> OnProcessStateChanged;

    public abstract StructureUIBase OpenStructureUI();
}

public abstract class StructureContextBase<TData> : StructureContextBase
    where TData : StructureDataBase
{
    public new TData Data => (TData)_data;

    protected StructureContextBase(TData data) : base(data) {

    }

    protected virtual bool TryGetStructure(out Structure structure) {
        return Managers.Structure.TryGetStructure(Data.ID, out structure);
    }
}
