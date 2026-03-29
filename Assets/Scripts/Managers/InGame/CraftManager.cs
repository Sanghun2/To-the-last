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

    public RecipeSD Target => craftTarget;
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

    //public float CurrentValue => currentValue;
    //public float MaxValue => maxValue;

    public event Action<State, State> OnStateChanged;
    //public event ActionData<float, float> OnValueChanged;

    [SerializeField] RecipeSD craftTarget;
    [SerializeField] State _currentState;
    //private float currentValue;
    //private float maxValue;

    public void SetTarget(RecipeSD recipeSD) {
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
    public RecipeSD CraftTarget => craftContext == null ? null : craftContext.Target;
    public CraftContext CraftContext => craftContext;

    [SerializeField] CraftContext craftContext = new CraftContext();

    public event Action<RecipeSD> OnTargetSet;

    public void SetCraftTarget(RecipeSD recipeSD) {
        if (craftContext.CanSelect) {
            craftContext.SetTarget(recipeSD);
            OnTargetSet?.Invoke(recipeSD);
            Debug.Log($"target set. {recipeSD.DisplayText}");
        }
    }

    public bool TryCraft(RecipeSD targetRecipeSD, Action<float, float> onProgress = null, Action onComplete = null) {
        SetCraftTarget(targetRecipeSD);
        if (craftContext.CanCraft) {
            craftContext.CurrentState = CraftContext.State.Crafting;
            FocusJob focusJob = new FocusJob(
                targetRecipeSD.RequireMinutes,
                onProgress,
                () => {
                    onComplete?.Invoke();
                });

            Managers.Job.DoFocusJob(focusJob);
            return true;
        }

        return false;
    }

    public void RegisterDelayedJob(RecipeSD recipeSD, Action<float, float> onProgress = null) {
        if (recipeSD is DelayedRecipeSD delayedRecipeSD) {
            var newJob = new Job(
                delayedRecipeSD.CompletionDelayMinutes,
                onProgress,
                () => craftContext.CurrentState = CraftContext.State.Completed);
            Managers.Job.RegisterDelayedJob(newJob);
        }
    }

    public void ClaimCraftResult() {
        if (craftContext.CurrentState == CraftContext.State.Completed) {
            // 아이템 지급 처리
            Ingredient output = craftContext.Target.Outputs.First();
            ItemSD itemSD = output.ItemSD;
            int amount = output.Amount;
            ItemStack inputStack = new ItemStack(new ItemData(itemSD.ID, itemSD.MaxStackCount), amount);
            if (Managers.Item.TryPushItem(Managers.Player.Inventory, inputStack, out var overflowedStack)) {
                Debug.Log($"{CraftTarget.DisplayText} 획득");
            }

            craftContext.ClearTarget();
        }
    }
}
