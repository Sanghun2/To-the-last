using TMPro;
using UnityEngine;

public class ConstructionContentUI : ContentUIBase<StructureSDBase>
{
    [SerializeField] StructureInfoButton infoButton;
    [SerializeField] RequirementUIContainer requirementUIContainer;

    public override void InitContent(StructureSDBase structureSD) {
        Debug.Log($"count? {structureSD.Requirements.Count}");
        base.InitContent(structureSD);
        infoButton.SetData(structureSD.ID);
        requirementUIContainer.ShowRequirements(structureSD.Requirements);

        executionButton.SetExecuteAction(new ActionData(
            $"제작\n({structureSD.ConstructionTime}분)",
            () => {
                var hasEnoughIngredients = InventoryUtility.HasIngredients(structureSD.Requirements);
#if TEST
                hasEnoughIngredients = true;
#endif
                if (hasEnoughIngredients) {
                    Managers.Construction.SetTargetStructure(structureSD);
                    Managers.Construction.ConstructCurrentTarget(
                        onProgress: progressBarUI.UpdateUI,
                        onComplete: () => {
                            progressBarUI.Clear();
                            Managers.UI.CloseUI<ConstructionUI>();
                        });
                }
                else {
                    Debug.LogAssertion($"재료 불충분");
                }
            }
            ));
    }
}
