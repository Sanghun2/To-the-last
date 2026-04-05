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
                   var context = targetUI.Structure.StructureContext;
                   if (context != null) {
                       context.ProcessState = Structure.ProcessState.Processing;
                   }
                   
                   targetUI.ExecutionButton.SetExecuteAction("획득");
               },
               onProgress: targetUI.ProgressBarUI.UpdateUI,
               onComplete: () => {
                   targetUI.ProgressBarUI.Clear();
                   targetUI.Structure.StructureContext.ProcessState = Structure.ProcessState.Available;
                   targetUI.ExecutionButton.SetExecuteAction(new ActionData(
                       "획득",
                       () => {
                           if (!Managers.Inventory.TryGetInventoryByTag(Define.Tag.PLAYER, out var inventories)) { return; }
                           var createdItem = CreateItem(contentContext.ID, contentContext.Amount);

                           if (InventoryUtility.TryPushItem(createdItem, inventories, true)) {
                               targetUI.ExecutionButton.SetExecuteAction(targetUI.DefaultAction);
                           }
                       }));
               });
        Managers.Job.RegisterDelayedJob(newJob);
        return true;
    }






    //public void SetCraftTarget(ProductionContentSD recipeSD) {
        //var currentTarget = craftContext.Target;
        //if (currentTarget != null && currentTarget.Equals(recipeSD)) return;

        //if (craftContext.CanSelect) {
        //    craftContext.SetTarget(recipeSD);
        //    OnCraftTargetSet?.Invoke(recipeSD);
        //    //Debug.Log($"target set. {recipeSD.DisplayText}");
        //}
    //}

    //public bool TryCraft(ProductionContentSD targetRecipeSD,
    //    Action onStartProgress=null,
    //    Action<float, float> onProgress = null, 
    //    Action onComplete = null) {

    //    SetCraftTarget(targetRecipeSD);
    //    if (craftContext.CanCraft) {
    //        craftContext.CurrentState = CraftContext.State.Crafting;
    //        FocusJob focusJob = new FocusJob(
    //            targetRecipeSD.RequireMinutes,
    //            onStart: onStartProgress,
    //            onProgress: onProgress,
    //            onComplete: onComplete).WithBlockScreen();

    //        Managers.Job.DoFocusJob(focusJob);
    //        return true;
    //    }

    //    return false;
    //}

    public void RegisterDelayedJob(ProductionContentSD recipeSD, Action<float, float> onProgress = null) {
        if (recipeSD is DelayedProductionContentSD delayedRecipeSD) {
            var newJob = new Job(
                delayedRecipeSD.RequireMinutesToComplete,
                onProgress: onProgress);
            Managers.Job.RegisterDelayedJob(newJob);
        }
    }

    //public void ClaimCraftResult() {
    //    if (craftContext.CurrentState == CraftContext.State.Completed) {
    //        // 아이템 지급 처리
    //        Ingredient output = craftContext.Target.Outputs.First();
    //        ItemSD itemSD = output.ItemSD;
    //        int amount = output.Amount;
    //        ItemStack inputStack = new ItemStack(new ItemData(itemSD.ID, itemSD.MaxStackCount), amount);
    //        if (Managers.Item.TryPushItem(Managers.Player.Inventory, inputStack, out var overflowedStack)) {
    //            Debug.Log($"{CraftTarget.DisplayText} 획득");
    //        }

    //        craftContext.ClearTarget();
    //    }
    //}
}
