using System;
using BilliotGames;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatUIBase : UIBase
{
    public Define.Stat StatType => statSD.StatType;  
    [SerializeField] protected StatSD statSD;

    public override void InitUI() {
        if (IsInit) return;

        Managers.Player.Player.RegisterEvent(StatType, UpdateUI);

        _isInit = true;
    }

    public abstract void UpdateUI(Value<float> value);

    protected virtual void OnValidate() {
        if (statSD == null) return;
        string newName = CreateName(StatType);
        if (!newName.Equals(gameObject.name)) {
            gameObject.name = newName;
        }
    }

    protected virtual string CreateName(Define.Stat targetStat) {
        return $"{targetStat} Stat UI";
    }
}
