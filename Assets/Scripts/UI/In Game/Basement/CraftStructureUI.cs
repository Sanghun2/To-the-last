using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CraftStructureUI : StructureUIBase<ProductionStructureContext>, IUpgradeableUI
{
    public Structure Structure => structure;
    public StructureUpgradeUI UpgradeUI => upgradeUI;
    private ProductionStructureContext Context => structure.StructureContext as ProductionStructureContext;

    [SerializeField] StructureUpgradeUI upgradeUI;
    [SerializeField] ProductionContentUIContainer productionContentUIContainer;


    private StructureDataParserContainer dataParserContainer = new StructureDataParserContainer();
    private Structure structure;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        productionContentUIContainer.Clear();
        upgradeUI.InitUI();

        _isInit = true;
    }
    public override void SetUpUI(Structure structure) {
        InitUI();
        this.structure = structure;
        productionContentUIContainer.SetStrcture(structure);
        var structureContext = Context;

        if (structureContext != null) {
            var productions = structureContext.Data.Prouctions;
            InitExecutionButtonTexts(structure.DefaultExecutionButtonText, productions);
            titleText.SetText(structureContext.DisplayText);
            productionContentUIContainer.ShowContents(productions);
        }

        structure.OnUpgraded -= UpdateUpgradeInfo;
        structure.OnUpgraded += UpdateUpgradeInfo;
        UpdateUpgradeInfo(structure);

        structure.OnUpgraded -= UpdateContentView;
        structure.OnUpgraded += UpdateContentView;
        UpdateContentView();
    }

    private void OnEnable() {
    }
    private void OnDisable() {
        if (structure != null) {
            structure.OnUpgraded -= UpdateUpgradeInfo;
            structure.OnUpgraded -= UpdateContentView;
            structure = null;
        }
    }

    private void UpdateContentView() => UpdateContentView(null);
    private void UpdateContentView(Structure _) {
        productionContentUIContainer.ShowContents(Context.Data.Prouctions);
    }

    private void UpdateUpgradeInfo(Structure structure) {
        SetTitleText($"{structure.DisplayText}");
        Upgrade.InfoResult result = Managers.Upgrade.TryGetNextUpgradeInfo(structure, out StructureSDBase nextUpgrade);

        switch (result) {
            case Upgrade.InfoResult.InValid:
            case Upgrade.InfoResult.MaxLevel:
                upgradeUI.SetUpMaxUpgrade(structure.StructureContext.Data);
                break;
            case Upgrade.InfoResult.Available:
                if (!dataParserContainer.TryGet(nextUpgrade, out var parser)) return;
                upgradeUI.SetUpUpgradeInfo(parser.ParseData(nextUpgrade));
                break;
            default:
                break;
        }
    }
}
