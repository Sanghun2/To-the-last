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
    public int ExpensionLevel => expensionLevel;


    [SerializeField] int expensionLevel;
    [SerializeField] Structure structure = new Structure();
    [SerializeField] ObjectActivator objectActivator;
    [SerializeField] Image structureImage;
    private int index;
    private Dictionary<Structure.StructureState, ActionBase> stateActions = new Dictionary<Structure.StructureState, ActionBase>();

    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();

        UpdateObject(structure.CurrentState, structure.CurrentState);

        var requirements = GetRequirementsToExpension();
        RegisterAction(Structure.StructureState.Locked, new ShowInfomationAction(new ExpensionPopUpData(
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
            )));

        if (Managers.SD.TryGetContainer<UpgradeSDBase>(out var container)) {
            List<UpgradeSDBase<StructureSD>> upgradeSDBases = container.SDDict.Where(x => {
                var structureSD = x.Value as UpgradeSDBase<StructureSD>;
                return structureSD != null;
            }).Select(x => (x.Value as UpgradeSDBase<StructureSD>)).ToList();

            var constructionContext = new ConstructionContext(index, upgradeSDBases);
            RegisterAction(Structure.StructureState.Empty, new ShowConstructionUIAction(constructionContext));
        }
        RegisterAction(Structure.StructureState.Built, new ShowStructureUIAction(structure));

        _isInit = true;
    }


    private void OnEnable() {
        structure.OnStateChanged -= UpdateObject;
        structure.OnStateChanged += UpdateObject;
    }
    private void OnDisable() {
        structure.OnStateChanged -= UpdateObject;
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
}
