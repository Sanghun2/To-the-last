using UnityEngine;

[CreateAssetMenu(fileName = "DelayedProductionContentSD", menuName = "Scriptable Objects/Content/DelayedProductionContentSD")]
public class DelayedProductionContentSD : ProductionContentSD
{
    public int RequireMinutesToComplete => requireMinutesToComplete;

    [SerializeField] int requireMinutesToComplete;

    protected override void OnValidate() {
        RenameAsset(ID, suffix:"_DelayedRecipeSD");
    }
}
