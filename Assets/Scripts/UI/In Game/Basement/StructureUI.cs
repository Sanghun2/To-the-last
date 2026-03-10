using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using BilliotGames;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class StructureUI : ButtonBase
{
    public int Index => index;


    public Structure Structure => structure;

    [SerializeField] Structure structure = new Structure();
    [SerializeField] ObjectActivator objectActivator;
    [SerializeField] Image structureImage;
    private int index;
    private Dictionary<Structure.StructureState, ActionBase> stateActions = new Dictionary<Structure.StructureState, ActionBase>();

    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();

        structure.OnStateChanged -= UpdateObject;
        structure.OnStateChanged += UpdateObject;
        UpdateObject(structure.CurrentState, structure.CurrentState);

        RegisterAction(Structure.StructureState.Locked, new ShowInfomationAction(new InfomationPopUpData(
            "구역 확장",
            "장애물을 제거하고 구역을 확장하시겠습니까?",
            new ActionData[] {
                new ActionData("취소", () => Managers.UI.CloseUI<InfomationPopUpUI>()),
                new ActionData("확장", () => {
                    Managers.UI.CloseUI<InfomationPopUpUI>();
                    structure.Unlock();
                })
            })));

        if (Managers.SD.TryGetContainer<StructureSD>(out var container)) {
            var structureList = container.SDDict.Select(x => x.Value).ToList();
            var constructionContext = new ConstructionContext(index, structureList);
            RegisterAction(Structure.StructureState.Empty, new ShowConstructionUIAction(constructionContext));
        }
        RegisterAction(Structure.StructureState.Built, new ShowUIAction(structure));

        _isInit = true;
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
            structureImage.sprite = structure.StructureSD.IconImage;
        }
    }
}
