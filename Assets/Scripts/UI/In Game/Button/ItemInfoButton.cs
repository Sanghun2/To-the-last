using System.Collections;
using BilliotGames;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInfoButton : ButtonBase, IPointerDownHandler, IPointerUpHandler
{
    // 길게 누르기 판단 기준 시간 (초)
    private const float LONG_PRESS_DURATION = 0.25f;

    private ItemData itemData;

    // 아이템을 이동시킬 인벤토리
    // 외부에서 SetInventory로 주입받아 사용
    private InventoryBase fromInventory;
    private InventoryBase toInventory;

    private bool isPointerDown;
    // 길게 누르기가 이미 발동됐으면 short press(이동) 동작을 막기 위한 플래그
    private bool longPressTriggered;
    private Coroutine longPressCoroutine;

    public void SetItemData(ItemData data) {
        itemData = data;
    }

    public void SetInventory(InventoryBase from, InventoryBase to) {
        fromInventory = from;
        toInventory = to;
    }

    public void OnPointerDown(PointerEventData eventData) {
        isPointerDown = true;
        longPressTriggered = false;
        // 누르는 순간 코루틴 시작 → 1초 후 자동 발동
        longPressCoroutine = StartCoroutine(LongPressRoutine());
    }

    public void OnPointerUp(PointerEventData eventData) {
        // 손 떼면 코루틴 취소
        if (longPressCoroutine != null) {
            StopCoroutine(longPressCoroutine);
            longPressCoroutine = null;
        }

        if (!isPointerDown) return;
        isPointerDown = false;

        // 길게 누르기가 이미 발동됐으면 short press 동작 무시
        if (longPressTriggered) return;

        // 1초 미만으로 눌렀다 뗀 경우 → 아이템 1개 이동
        TryMoveOneItem();
    }

    // ButtonBase의 ButtonAction은 OnPointerDown/Up에서 직접 처리하므로
    // 기본 클릭 동작은 비워둠
    protected override void ButtonAction() { }

    private IEnumerator LongPressRoutine() {
        yield return new WaitForSeconds(LONG_PRESS_DURATION);
        // 1초 경과 → 길게 누르기 발동
        longPressTriggered = true;
        ShowItemInfo();
    }

    private void TryMoveOneItem() {
        if (itemData == null) { Debug.Log("타겟 아이템 없음"); return; }
        if (fromInventory == null || toInventory == null) {
            Debug.LogError("인벤토리가 연결되지 않음");
            return;
        }

        // 이동할 아이템이 fromInventory에 있는지 확인
        int currentCount = fromInventory.GetItemCount(itemData.ItemID);
        if (currentCount <= 0) { Debug.Log("이동할 아이템 없음"); return; }

        // 1개짜리 새 스택을 만들어서 toInventory에 push
        // 직접 원본 스택을 넘기지 않는 이유: Amount=0이 되면 ReleaseItem이 호출되기 때문
        var moveStack = new ItemStack(itemData, 1);
        if (toInventory.TryPushItem(moveStack, out var overflow)) {
            // push 성공한 만큼만 fromInventory에서 제거
            // overflow가 있으면 실제로 들어간 양은 1 - overflow.Amount
            int movedAmount = 1 - (overflow?.Amount ?? 0);
            if (movedAmount > 0) {
                fromInventory.TryRemoveItem(itemData.ItemID, movedAmount);
            }
        }
    }

    private void ShowItemInfo() {
        if (itemData == null) { Debug.Log("타겟 아이템 없음"); return; }
        if (!Managers.SD.TryGetSD(itemData.ItemID, out ItemSD itemSD)) { return; }

        var infoUI = Managers.UI.GetUI<InfomationPopUpUI>();
        if (infoUI.IsOpened) return;

        InfomationPopUpData infoData = new InfomationPopUpData(
            itemSD.DisplayText,
            itemSD.Description,
            new ActionData[] { new ActionData("확인", () => Managers.UI.CloseUI(infoUI)) },
            image: itemSD.Image);
        Managers.UI.OpenUI(infoUI).InitPopUp(infoData);
    }
}