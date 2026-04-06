using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class UtilityStructureUI : StructureUIBase<UtilityStructureContext>, IUpgradeableUI
{
    public StructureUpgradeUI UpgradeUI => upgradeUI;
    private UtilityStructureContext Context => structure.StructureContext as UtilityStructureContext;

    [SerializeField] StructureUpgradeUI upgradeUI;
    [SerializeField] ActivityContentUIContainer activityContentUIContainer;
    private Structure structure;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }
    public override void SetUpUI(Structure structure) {
        InitUI();

        this.structure = structure;
        var structureContext = Context;
        if (structureContext != null) {
            activityContentUIContainer.SetStructure(structure);
            var contents = structureContext.ContentList;
            InitExecutionButtonTexts(structure.DefaultExecutionButtonText, contents);
            SetTitleText(structureContext.DisplayText);
            activityContentUIContainer.ShowContents(contents);
        }
        else {
            Debug.LogError($"({structure.StructureContext}) is not type of ({typeof(UtilityStructureContext)})");
        }

        UpdateUpgradeUI(structure);

        structure.OnUpgraded -= UpdateUpgradeUI;
        structure.OnUpgraded += UpdateUpgradeUI;

        structure.OnUpgraded -= UpdateContentView;
        structure.OnUpgraded += UpdateContentView;
    }



    protected virtual void OnDisable() {
        if (structure != null) {
            structure.OnUpgraded -= UpdateUpgradeUI;
            structure.OnUpgraded -= UpdateContentView;
            structure = null;
        }
    }
    private void UpdateUpgradeUI(Structure structure) {
        SetTitleText($"{structure.DisplayText}");
        var upgradeInfoResult = Managers.Upgrade.TryGetNextUpgradeInfo(structure, out StructureSDBase nextUpgrade);
        switch (upgradeInfoResult) {
            case Upgrade.InfoResult.InValid:
            case Upgrade.InfoResult.MaxLevel:
                upgradeUI.SetUpMaxUpgrade(structure.StructureContext.Data);
                break;
            case Upgrade.InfoResult.Available:
                if (!Managers.Construction.StructureDataParserContainer.TryGet(nextUpgrade, out var parser)) { Debug.LogError($"<color=red>({nextUpgrade.GetType()}) data parser is not exist</color>"); return; }
                upgradeUI.SetUpUpgradeInfo(parser.ParseData(nextUpgrade));
                break;
            default:
                break;
        }
    }
    private void UpdateContentView(Structure _) {
        activityContentUIContainer.ShowContents(Context.ContentList);
    }
}
