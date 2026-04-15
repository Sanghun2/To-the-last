using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] float longPressDuration = 0.5f;
    private Action longPressAction;
    private Coroutine _longPressCoroutine;

    public bool IsLongPressTriggered { get; private set; }

    public void SetLongPressAction(Action action) {
        longPressAction = action;
    }

    public void OnPointerDown(PointerEventData eventData) {
        IsLongPressTriggered = false;
        _longPressCoroutine = StartCoroutine(LongPressRoutine());
    }

    public void OnPointerUp(PointerEventData eventData) {
        CancelLongPress();
    }

    public void OnPointerExit(PointerEventData eventData) {
        CancelLongPress();
    }

    private IEnumerator LongPressRoutine() {
        yield return new WaitForSeconds(longPressDuration);
        IsLongPressTriggered = true;
        longPressAction?.Invoke();
    }

    private void CancelLongPress() {
        if (_longPressCoroutine != null) {
            StopCoroutine(_longPressCoroutine);
            _longPressCoroutine = null;
        }
    }
}