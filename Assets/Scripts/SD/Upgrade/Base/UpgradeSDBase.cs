using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public abstract class UpgradeSDBase : SDBase
{
    public abstract IUpgradeable GetFirstUpgradeable();

    public abstract Upgrade.InfoResult TryGetUpgradeInfo(int level, out IUpgradeable upgrade);

    protected virtual void OnValidate() {
        if (Categories != null && Categories.Count > 0) {
            id = Categories[0].ID;
        }

        RenameAsset(ID, suffix: "_UpgradeSD");
    }
}

public abstract class UpgradeSDBase<TUpgradeable> : UpgradeSDBase
    where TUpgradeable : IUpgradeable
{
    public IReadOnlyList<TUpgradeable> UpgradeList => upgradeList;

    [SerializeField] List<TUpgradeable> upgradeList = new List<TUpgradeable>();

    public override IUpgradeable GetFirstUpgradeable() {
        if (upgradeList != null && upgradeList.Count > 0) {
            return upgradeList[0];
        }

        return default;
    }
    public virtual TUpgradeable GetFirstUpgrade() {
        if (upgradeList != null && upgradeList.Count > 0) {
            return upgradeList[0];
        }

        return default;
    }

    public override Upgrade.InfoResult TryGetUpgradeInfo(int level, out IUpgradeable upgrade) {
        var result = TryGetUpgradeInfo(level, out TUpgradeable tUpgrade);
        upgrade = tUpgrade;
        return result;
    }
    public virtual Upgrade.InfoResult TryGetUpgradeInfo(int level, out TUpgradeable upgrade) {
        upgrade = default;
        if (level < 0) { return Upgrade.InfoResult.InValid; }
        if (level >= upgradeList.Count) { return Upgrade.InfoResult.MaxLevel; }

        upgrade = upgradeList[level];
        return Upgrade.InfoResult.Available;
    }
}

public class Upgrade
{
    public enum InfoResult {
        InValid,
        MaxLevel,
        Available
    }
}