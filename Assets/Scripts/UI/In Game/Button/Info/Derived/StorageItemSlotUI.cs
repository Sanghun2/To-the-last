using System.Collections;
using BilliotGames;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageItemSlotUI : InfoButtonBase
{
    [SerializeField] LongPressHandler longPressHandler;

    // 아이템을 이동시킬 인벤토리
    // 외부에서 SetInventory로 주입받아 사용
    private InventoryBase fromInventory;
    private InventoryBase toInventory;


    public override void InitUI() {
        if (IsInit) return;
        base.InitUI();

        if (longPressHandler != null) {
            longPressHandler.SetLongPressAction(ShowInfomation);
        }

        _isInit = true;
    }

    public void SetInventory(InventoryBase from, InventoryBase to) {
        fromInventory = from;
        toInventory = to;
    }


    // ButtonBase의 ButtonAction은 OnPointerDown/Up에서 직접 처리하므로
    // 기본 클릭 동작은 비워둠
    protected override void ButtonAction() { 
        if (longPressHandler?.IsLongPressTriggered == false) {
            TryMoveOneItem();
            Debug.Log("single touched");
        }
    }

    private void TryMoveOneItem() {
        if (string.IsNullOrEmpty(dataID)) { Debug.Log("타겟 아이템 없음"); return; }
        if (fromInventory == null || toInventory == null) {
            Debug.LogError("인벤토리가 연결되지 않음");
            return;
        }

        // 이동할 아이템이 fromInventory에 있는지 확인
        int currentCount = fromInventory.GetItemCount(dataID);
        if (currentCount <= 0) { Debug.Log("이동할 아이템 없음"); return; }

        // 1개짜리 새 스택을 만들어서 toInventory에 push
        // 직접 원본 스택을 넘기지 않는 이유: Amount=0이 되면 ReleaseItem이 호출되기 때문
        var moveStack = new ItemStack(new ItemData(dataID, 999), 1);
        if (toInventory.TryPushItem(moveStack, out var overflow)) {
            // push 성공한 만큼만 fromInventory에서 제거
            // overflow가 있으면 실제로 들어간 양은 1 - overflow.Amount
            int movedAmount = 1 - (overflow?.Amount ?? 0);
            if (movedAmount > 0) {
                fromInventory.TryRemoveItem(dataID, movedAmount);
            }
        }
    }
}