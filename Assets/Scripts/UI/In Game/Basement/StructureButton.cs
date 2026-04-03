using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class StructureButton : ButtonBase
{
    public int Index => index;
    public Structure Structure => structure;

    [SerializeField] int expensionLevel;
    [SerializeField] Structure structure = new Structure();
    [SerializeField] ObjectActivator objectActivator;
    [SerializeField] Image structureImage;
    [SerializeField] IconUIContainer iconUIContainer;
    private int index;
    private Dictionary<Structure.StructureState, ActionBase> stateActions = new Dictionary<Structure.StructureState, ActionBase>();

    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();

        structure.SetExpensionLevel(expensionLevel);
        UpdateObject(structure.CurrentState, structure.CurrentState);
        iconUIContainer.InitUI();

        RegisterAction(Structure.StructureState.Locked, CreateActionOnLocked());
        RegisterAction(Structure.StructureState.Empty, CreateActionOnEmpty());
        RegisterAction(Structure.StructureState.Built, CreateActionOnBuilt());

        _isInit = true;
    }

    private void OnEnable() {
        structure.OnStructureStateChanged -= UpdateObject;
        structure.OnStructureStateChanged += UpdateObject;

        structure.OnUpgradeAvailabilityChanged -= UpdateUpgradeIcon;
        structure.OnUpgradeAvailabilityChanged += UpdateUpgradeIcon;

        structure.OnProductionCompleted -= UpdateProductionIcon;
        structure.OnProductionCompleted += UpdateProductionIcon;

        structure.SubscribeUpgradeEvents();
    }

    private void OnDisable() {
        structure.OnStructureStateChanged -= UpdateObject;
        structure.OnUpgradeAvailabilityChanged -= UpdateUpgradeIcon;
        structure.UnsubscribeUpgradeEvents();
    }

    public void RegisterAction(Structure.StructureState state, ActionBase buttonAction) {
        stateActions[state] = buttonAction;
    }

    protected override void ButtonAction() {
        if (stateActions.TryGetValue(structure.CurrentState, out ActionBase action)) {
            action.Execute();
        }
    }

    internal void AssignIndex(int index) {
        this.index = index;
    }

    private void UpdateObject(Structure.StructureState currentState, Structure.StructureState prevState) {
        objectActivator.ShowObject((int)currentState);

        if (currentState == Structure.StructureState.Built) {
            structureImage.sprite = structure.StructureContext.StructureImage;
        }
    }
    private IReadOnlyList<Ingredient> GetRequirementsToExpension() {
        if (Managers.SD.TryGetSD($"level{expensionLevel}", out ExpensionSD targetSD)) {
            return targetSD.Requirements;
        }

        return null;
    }

    private ActionBase CreateActionOnLocked() {
        var requirements = GetRequirementsToExpension();
        return new ShowInfomationAction(new ExpensionPopUpData(
            "구역 확장",
            "장애물을 제거하고 구역을 확장하시겠습니까?",
            requirements,
            new ActionData[] {
                new ActionData("취소", () => Managers.UI.CloseUI<InfomationPopUpUI>()),
                new ActionData("확장", () => {
                    Managers.UI.CloseUI<InfomationPopUpUI>();
                    structure.Unlock();
                },
                () => InventoryUtility.HasIngredients(requirements)
                )
            }
            ));
    }
    private ActionBase CreateActionOnEmpty() {
        if (!Managers.SD.TryGetContainer<UpgradeSDBase>(out var container)) { return null; }

        // upgrade SD data에서 가장 1렙 구조물 데이터만 추출
        List<UpgradeSDBase<StructureSD>> upgradeSDBases = container.SDDict.Where(x => {
            var structureSD = x.Value as UpgradeSDBase<StructureSD>;
            return structureSD != null;
        }).Select(x => (x.Value as UpgradeSDBase<StructureSD>)).ToList();

        var constructionContext = new ConstructionContext(index, upgradeSDBases);

        return new ShowConstructionUIAction(constructionContext);
    }
    private ActionBase CreateActionOnBuilt() {
        return new ShowStructureUIAction(structure);
    }

    private void UpdateUpgradeIcon(bool canUpgrade) {
        iconUIContainer.ActiveIcon(Define.Icon.UPGRADE_READY, canUpgrade);
    }

    private void UpdateProductionIcon(ProductionResult productionResult) {
        iconUIContainer.ActiveIcon(Define.Icon.PRODUCTION_COMPLETE, productionResult.IsEmpty);
    }
}
