using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class StructureUI : ButtonBase
{
    public enum State {
        Empty,
        Built,
    }

    public State StructureState => structureState;

    [SerializeField] ObjectActivator objectActivator;
    [SerializeField] Image structureImage;
    private State structureState;

    public void InitStructure(State state, StructureSD structureSD) {
        structureState = state;
        switch (state) {
            case State.Empty:
                objectActivator.ShowObject(0);
                break;
            case State.Built:
                objectActivator.ShowObject(1);
                structureImage.sprite = structureSD.IconImage;
                break;
            default:
                break;
        }
    }

    protected override void ButtonAction() {
        if (structureState == State.Empty) {
            Managers.UI.OpenUI<BuildingUI>();
        }
        else if (structureState == State.Built) {

        }
    }
}
