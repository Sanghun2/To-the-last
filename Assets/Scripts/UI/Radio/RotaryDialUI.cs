using UnityEngine;
using UnityEngine.EventSystems;
using System;
using BilliotGames;

public class RotaryDial : UIBase, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Angle Limit")]
    [SerializeField] private float maxAngle = 2000;
    [SerializeField] private float minAngle = -2000;

    [Header("Settings")]
    [SerializeField] private float deadZoneAngle = 0.5f;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float _degreesPerUnit = 1f;

    public float DegreesPerUnit => _degreesPerUnit;

    public event Action<float, float> OnValueChanged;
    public float Value { get; private set; }

    private float _totalAngle;
    private Vector2 _prevLocalPoint;
    private bool _isDragging;
    private RectTransform _rectTransform;

    public override void InitUI() {
        if (IsInit) return;

        _rectTransform = GetComponent<RectTransform>();
        _isInit = true;
    }

    // 외부에서 각도 제한 설정
    public void SetAngleRange(float min, float max) {
        minAngle = min;
        maxAngle = max;
    }

    public void OnDrag(PointerEventData e) {
        if (!_isDragging) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform, e.position, e.pressEventCamera, out Vector2 currentLocal);

        Vector2 from = _prevLocalPoint.normalized;
        Vector2 to = currentLocal.normalized;

        float cross = from.x * to.y - from.y * to.x;
        float angle = Vector2.Angle(from, to);

        float deltaAngle = (cross >= 0 ? angle : -angle) * sensitivity;

        if (Mathf.Abs(deltaAngle) < deadZoneAngle)
            return;

        _totalAngle = Mathf.Clamp(_totalAngle + deltaAngle, minAngle, maxAngle);

        _prevLocalPoint = currentLocal;

        float newValue = _totalAngle / _degreesPerUnit;

        if (!Mathf.Approximately(newValue, Value)) {
            float deltaValue = newValue - Value;
            Value = newValue;
            OnValueChanged?.Invoke(Value, deltaValue);
        }

        _rectTransform.localRotation = Quaternion.Euler(0f, 0f, -_totalAngle);
    }

    public void OnPointerUp(PointerEventData e) {
        _isDragging = false;
    }

    public void OnPointerDown(PointerEventData e) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform, e.position, e.pressEventCamera, out Vector2 localPoint);

        _prevLocalPoint = localPoint;
        _isDragging = true;
    }
}