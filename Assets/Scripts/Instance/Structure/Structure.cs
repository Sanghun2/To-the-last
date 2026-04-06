using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[Serializable]
public class Structure : IValue<float>
{
    public enum StructureState
    {
        Locked,
        Empty,
        Built,
    }

    public bool CanContruct => CurrentStructureState == StructureState.Empty;
    public bool CanDestroy => CurrentStructureState == StructureState.Built;
    public bool IsLocked => CurrentStructureState == StructureState.Locked;
    public StructureContextBase StructureContext => structureContext;
    public string ID => StructureContext?.ID ?? string.Empty;
    public string DisplayText => structureContext.DisplayText;

    public float CurrentValue => currentProgress;
    public float MaxValue => maxProgress;
    public StructureState CurrentStructureState
    {
        get => _currentState;
        protected set
        {
            var prevState = _currentState;
            _currentState = value;
            if (_currentState != prevState) {
                OnStructureStateChanged?.Invoke(_currentState, prevState);
            }
        }
    }
    public int StructureLevel => structureLevel;
    public int ExpensionLevel => expenstionLevel;
    public string DefaultExecutionButtonText => structureContext.Data.DefaultExecutionButtonText;

    [SerializeField] StructureContextBase structureContext;
    [SerializeField] int structureLevel;
    [SerializeField] int expenstionLevel;
    [SerializeField][HideInInspector] StructureState _currentState;

    [SerializeField][HideInInspector] float currentProgress;
    [SerializeField][HideInInspector] float maxProgress;

    private List<(InventoryBase inventory, Action<ItemEventArgs> handler)> _upgradeHandlers
    = new List<(InventoryBase, Action<ItemEventArgs>)>();

    public event Action<StructureState, StructureState> OnStructureStateChanged;
    public event Action<Structure> OnUpgraded;
    public event Action<bool> OnUpgradeAvailabilityChanged;
    public event Action<ProductionResult> OnProductionCompleted;

    public Structure() {
        SetAsDefaultState();
    }

    public void SetStructure(StructureContextBase structureContext) {
        this.structureContext = structureContext;
        CurrentStructureState = StructureState.Built;

        // item 개수 변화에 따라 requirement 수치 update, 만약 업그레이드 가능한 경우 icon도 update
        SubscribeUpgradeEvents();


        // 현재 작업중인 내용이 있다면 완료됐을 때 icon 띄우록 추가
    }


    public void ApplyUpgrade(string id, StructureContextBase newContext) {
        structureLevel += 1;
        //structureID = id;
        structureContext = newContext;

        SubscribeUpgradeEvents();
        OnUpgraded?.Invoke(this);
    }

    public void SetExpensionLevel(int expensionLevel) {
        this.expenstionLevel = expensionLevel;
    }

    public void Unlock() {
        if (IsLocked == false) { Debug.Log("<color=yellow>Lock 상태가 아닌데 unlock 시도</color>"); return; }

        if (StructureContext != null) {
            StructureContext.ProcessState = Process.State.Wait;
        }

        CurrentStructureState = StructureState.Empty;
    }
    public void DestroyStrucure() {
        if (CanDestroy == false) { return; }
        CurrentStructureState = StructureState.Empty;
        StructureContext.ProcessState = Process.State.Wait;
        structureContext = null;
        //structureID = null;
    }

    private void SetAsDefaultState() {
        //structureID = null;
        currentProgress = 0;
        maxProgress = structureContext == null ? 1 : structureContext.ConstructionTime;
        CurrentStructureState = StructureState.Locked;
    }


    #region Event

    public void SubscribeUpgradeEvents() {
        var result = Managers.Upgrade.TryGetNextUpgradeInfo(this, out IUpgradeable nextUpgrade);
        if (result == Upgrade.InfoResult.Available) {
            SubscribeUpgradeEvents(nextUpgrade);

            OnUpgradeAvailabilityChanged?.Invoke(InventoryUtility.HasIngredients(nextUpgrade.Requirements));
        }
    }
    public void SubscribeUpgradeEvents(IUpgradeable nextUpgrade) {
        foreach (var requirement in nextUpgrade.Requirements) {
            var itemID = requirement.ItemSD.ID;
            var requiredAmount = requirement.Amount;

            Action<ItemEventArgs> handler = (args) =>
            {
                if (!itemID.Equals(args.itemID)) return;
                int current = InventoryUtility.GetItemCount(itemID);
                int prev = current - args.delta;
                bool wasEnough = prev >= requiredAmount;
                bool isEnough = current >= requiredAmount;
                if (wasEnough != isEnough) {
                    bool canUpgrade = InventoryUtility.HasIngredients(nextUpgrade.Requirements);
                    OnUpgradeAvailabilityChanged?.Invoke(canUpgrade);
                }
            };

            if (!Managers.Inventory.TryGetInventoryByTag(
                out var inventoryList, Define.Tag.PLAYER, Define.Tag.STORAGE)) continue;

            foreach (var inventory in inventoryList) {
                inventory.OnItemChanged += handler;
                _upgradeHandlers.Add((inventory, handler));
            }
        }
    }
    public void UnsubscribeUpgradeEvents() {
        foreach (var (inventory, handler) in _upgradeHandlers) {
            inventory.OnItemChanged -= handler;
        }
        _upgradeHandlers.Clear();
    }

    #endregion
}
