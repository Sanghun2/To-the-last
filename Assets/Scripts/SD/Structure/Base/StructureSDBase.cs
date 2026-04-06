using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public abstract class StructureSDBase : ContentSDBase, IUpgradeable
{
    public int ConstructionTime => requireMinutes;
    public bool Locked => locked;
    public string FirstCategory => categoryList != null && categoryList.Count > 0 ? categoryList[0].ID : string.Empty;
    public string DefaultExecitionButtonText => executionButtonText;

    [SerializeField] protected bool locked=true;


    public void LockConstruction(bool @lock) {
        locked = @lock;
    }
}
