using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager
{
    public Upgrade.InfoResult TryGetNextUpgradeInfo<TUpgradeable>(Structure structure, out TUpgradeable nextUpgrade) where TUpgradeable : IUpgradeable {
        var currentLevel = structure.Level;
        nextUpgrade = default;
        if (!Managers.SD.TryGetSD(structure.StructureContext.CategoryID, out UpgradeSDBase upgradeSD)) { return Upgrade.InfoResult.InValid; }

        var result = upgradeSD.TryGetUpgradeInfo(currentLevel + 1, out IUpgradeable upgrade);

        if (result == Upgrade.InfoResult.Available && upgrade is TUpgradeable casted) {
            nextUpgrade = casted;
            return Upgrade.InfoResult.Available;
        }

        return Upgrade.InfoResult.InValid;
    }

    public IReadOnlyList<(Ingredient, bool)> CheckUpgradeItems(UpgradeSDBase upgradeSD) {
        return null;
    }
}
