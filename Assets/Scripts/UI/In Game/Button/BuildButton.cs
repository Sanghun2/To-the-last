using BilliotGames;
using UnityEngine;

public class BuildButton : ButtonBase
{
    [SerializeField] int locationIndex;
    [SerializeField] StructureSDBase targetStructureSD;

    protected override void ButtonAction() {
        if (targetStructureSD != null) {
            // check ingredients

            // try remove ingredients
            // try construction
        }
        else {
            Debug.LogAssertion($"target structure가 없음");
        }
    }
}
