using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager
{
    public event Action<Structure> OnStructureUpgradeProcessCompleted;

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



    public bool TryUpgrade(Structure targetStructure, Action onStart=null, Action<float, float> onProgress=null, Action onComplete=null) {
        if (Managers.Upgrade.TryGetNextUpgradeInfo(targetStructure, out StructureSD nextUpgrade) == Upgrade.InfoResult.Available) {
            if (!Managers.Construction.StructureDataParserContainer.TryGet(nextUpgrade, out var dataParser)) { return false; }
            var data = dataParser.ParseData(nextUpgrade);
            if (!Managers.Construction.StructureContextBuilderContainer.TryGet(data, out var contextBuilder)) { return false; }
            if (!contextBuilder.TryBuildContext(data, out StructureContextBase newContext)) { return false; }

            var requirmentsToUpgrade = nextUpgrade.RequirementItems;
            if (!InventoryUtility.TryConsumeIngredients(InventoryUtility.GetInventoriesInBasement(), requirmentsToUpgrade)) { Debug.Log($"failed to consume ingredients in inventories"); return false; }

            var job = Managers.Job.CreateFocusJob(
                nextUpgrade.ConstructionTime,
                onStart: onStart,
                onProgress: onProgress,
                onComplete:() => {
                    targetStructure.ApplyUpgrade(nextUpgrade.ID, newContext);
                    onComplete?.Invoke();
                }
                ).WithBlockScreen();

            Managers.Job.DoFocusJob(job, () => {
                OnStructureUpgradeProcessCompleted?.Invoke(targetStructure);
            });

            return true;
        }
        else {
            // 다음 upgrade 정보 get failed
        }

        return false;
    }
}
