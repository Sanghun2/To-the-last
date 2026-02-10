using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BilliotGames;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class StructureUI : ButtonBase
{
    public enum State {
        Locked,
        Empty,
        Built,
    }

    public bool CanContruct => _currrentStructureState == State.Empty;
    public bool CanDestroy => _currrentStructureState == State.Built;
    public bool IsLocked => CurrentStructureState == State.Locked;
    public int Index => index;


    public StructureSD StructureSD => structureSD;
    public State CurrentStructureState
    {
        get => _currrentStructureState;
        protected set
        {
            var prevState = _currrentStructureState;
            _currrentStructureState = value;
            if (_currrentStructureState != prevState) {
                UpdateObject(_currrentStructureState);
            }
        }
    }


    [SerializeField] ObjectActivator objectActivator;
    [SerializeField] Image structureImage;
    private State _currrentStructureState;
    private StructureSD structureSD;
    private int index;
    private Dictionary<State, ActionBase> stateActions = new Dictionary<State, ActionBase>();

    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();
        SetAsDefaultState();
        RegisterAction(State.Locked, new ShowInfomationAction(new InfomationPopUpData(
            "구역 확장",
            "장애물을 제거하고 구역을 확장하시겠습니까?",
            new ActionData[] {
                new ActionData("취소", () => Managers.UI.CloseUI<InfomationPopUpUI>()),
                new ActionData("확장", () => {
                    Managers.UI.CloseUI<InfomationPopUpUI>();
                    UnlockUI();
                })
            })));

        RegisterAction(State.Empty, new ShowConstructionUIAction());

        _isInit = true;
    }


    public void InitStructure(State state, StructureSD structureSD) {
        CurrentStructureState = state;
        this.structureSD = structureSD;
        switch (state) {
            case State.Empty:
                break;
            case State.Built:
                structureImage.sprite = structureSD.IconImage;
                break;
            default:
                break;
        }
    }
    public void ClearStructure() {
        InitStructure(State.Empty, null);
    }
    public void UnlockUI() {
        CurrentStructureState = State.Empty;
    }

    public void RegisterAction(State state, ActionBase buttonAction) {
        stateActions[state] = buttonAction;
    }

    protected override void ButtonAction() {
        if (stateActions.TryGetValue(CurrentStructureState, out ActionBase action)) {
            action.Execute();
        }
    }


    private void SetAsDefaultState() {
        CurrentStructureState = State.Locked;
        UpdateObject(State.Locked);
    }
    private void UpdateObject(State state) {
        objectActivator.ShowObject((int)state);
    }
    internal void AssignIndex(int index) {
        this.index = index;
    }
}
