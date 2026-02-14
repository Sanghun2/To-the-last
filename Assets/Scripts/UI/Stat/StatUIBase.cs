using BilliotGames;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatUIBase : UIBase
{
    [SerializeField] protected Define.Stat targetStat;

    public override void InitUI() {
        if (IsInit) return;

        Managers.Player.Player.RegisterEvent(targetStat, UpdateUI);

        _isInit = true;
    }

    public abstract void UpdateUI(Value value);
}
