using System;
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

    protected StructureDataBase _data;

    public StructureContextBase(StructureDataBase data) {
        this._data = data;
    }


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
