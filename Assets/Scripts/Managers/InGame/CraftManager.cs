using System;
using System.Linq;
using BilliotGames;
using UnityEngine;

public class CraftContext
{
    public enum State
    {
        None,
        Selected,
        Crafting,
        Completed,
    }

    public bool CanSelect =>
        CurrentState != State.Crafting &&
        CurrentState != State.Completed;
    public bool CanCraft => CurrentState == State.Selected;

    public ProductionContentSD Target => craftTarget;
    public State CurrentState
    {
        get => _currentState;
        set
        {
            var prevState = _currentState;
            _currentState = value;
            if (_currentState != prevState) {
                OnStateChanged?.Invoke(_currentState, prevState);
            }
        }
    }

    public event Action<State, State> OnStateChanged;

    [SerializeField] ProductionContentSD craftTarget;
    [SerializeField] State _currentState;

    public void SetTarget(ProductionContentSD recipeSD) {
        if (!CanSelect) { Debug.Log($"<color=green>현재 새로운 제작법을 선택할 수 없음</color>"); return; }

        craftTarget = recipeSD;
        CurrentState = State.Selected;
    }
    public void ClearTarget() {
        craftTarget = null;
        CurrentState = State.None;
    }
}

public sealed class CraftManager
{
    //public ProductionContentSD CraftTarget => craftContext == null ? null : craftContext.Target;
    //public CraftContext CraftContext => craftContext;

    //[SerializeField] CraftContext craftContext = new CraftContext();
    private ProductionDataParserContainer productionDataParserContainer = new ProductionDataParserContainer();
    private ProductionContextBuilderContainer productionContextBuilderContainer = new ProductionContextBuilderContainer();
    private ProductionContextProcessorContainer productionContextProcessorContainer = new ProductionContextProcessorContainer();

    public event Action<ProductionContentSD> OnCraftTargetSet;
    public event Action<ProductionContextBase, ProductionContentUI> OnCraftStarted;

    public bool TryCraftProduction(ProductionContentSD targetContentSD, ProductionContentUI targetUI) {
        if (!productionDataParserContainer.TryGet(targetContentSD, out var dataParser)) { Debug.LogError($"<color=red>({targetContentSD.GetType()}) data parser is not exist</color>"); return false; }
        var contentData = dataParser.ParseData(targetContentSD);

        return TryCraftProduction(contentData,targetUI);
    }
    public bool TryCraftProduction(ProductionDataBase contentData, ProductionContentUI targetUI) {
        if (!productionContextBuilderContainer.TryGet(contentData, out var contextBuidler)) { return false; }
        var contentContext = contextBuidler.BuildContext(contentData);

        if (!productionContextProcessorContainer.TryGet(contentContext, out var processor)) { return false; }

        return processor.TryCraft(contentContext, targetUI);
    }

    public ItemStack CreateItem(string id, int amount) {
        return new ItemStack(new ItemData(id, 999), amount);
    }
    public bool TryRegisterDelayedProduction(DelayedProductionContext contentContext, ProductionContentUI targetUI) {
        if (contentContext == null) { Debug.LogError($"<color=red>context is null</color>"); return false; }
        var newJob = new Job(
               contentContext.RequireMinutesToComplete,
               onStart: () => {
                   targetUI.ExecutionButton.SetExecuteAction("획득");
                   var context = targetUI.Structure.StructureContext;
                   if (context != null) {
                       context.ProcessState = Process.State.InProgress;
                   }
               },
               onProgress: targetUI.ProgressBarUI.UpdateUI,
               onComplete: () => {
                   targetUI.ProgressBarUI.Clear();
                   targetUI.Structure.StructureContext.ProcessState = Process.State.Wait;
                   targetUI.ExecutionButton.SetExecuteAction(new ActionData(
                       "획득",
                       () => {
                           if (!Managers.Inventory.TryGetInventoryByTag(Define.Tag.PLAYER, out var inventories)) { return; }
                           var createdItem = CreateItem(contentContext.ID, contentContext.Amount);

                           if (InventoryUtility.TryPushItem(inventories, createdItem, true)) {
                               targetUI.ExecutionButton.SetExecuteAction(targetUI.DefaultAction);
                           }
                       }));
               });
        Managers.Job.RegisterDelayedJob(newJob);
        return true;
    }

    public void RegisterDelayedJob(ProductionContentSD recipeSD, Action<float, float> onProgress = null) {
        if (recipeSD is DelayedProductionContentSD delayedRecipeSD) {
            var newJob = new Job(
                delayedRecipeSD.RequireMinutesToComplete,
                onProgress: onProgress);
            Managers.Job.RegisterDelayedJob(newJob);
        }
    }
}
