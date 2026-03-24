using UnityEngine;

public class InGameCanvas : CanvasBase
{
    [SerializeField] BasementUI basementUI;

    public override void InitUI() {
        basementUI.InitUI();

    }
}
