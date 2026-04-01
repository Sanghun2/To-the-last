using BilliotGames;
using UnityEngine;

public abstract class UpgradeUIBase : UIBase
{

}

public abstract class UpgradeUIBase<TData> : UpgradeUIBase
{
    public abstract void SetUpUpgradeInfo(TData data);
}