using BilliotGames;
using UnityEngine;

public abstract class BatteryUIBase : UIBase
{
    public abstract void UpdateGaugeUI(float currentValue, float maxValue);
}
